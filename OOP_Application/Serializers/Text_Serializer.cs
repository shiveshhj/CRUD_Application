using OOP_Application.Serializable_Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Reflection;

namespace OOP_Application.Serializers
{
    internal class Text_Serializer : ISerializer
    {
        public void Serialize(List<Vehicle> vehicles, string fileName)
        {
            List<VehicleSerializableModel> models = ClassConverter.VehiclesToModels(vehicles);
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("[");
                foreach (var model in models)
                {
                    Type modelType = model.GetType();
                    writer.WriteLine($"~{modelType.Name.Replace("SerializableModel", "")}");
                    foreach (var field in modelType.GetFields())
                    {
                        var value = field.GetValue(model);
                        if (field.FieldType.IsPrimitive || field.FieldType == typeof(string) || field.FieldType.IsEnum)
                        {
                            writer.WriteLine($"\t{field.Name} <=> {value}");
                        }
                        else if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            OutputNestedList(value, writer, field);
                        }
                        else
                        {
                            OutputNestedObject(value, writer, field);
                        }
                    }
                    writer.WriteLine("---");
                }
                writer.Write("]");
            }
        }

        private void OutputNestedObject(object value, StreamWriter writer, FieldInfo field)
        {
            Type nestedObjectType = field.FieldType;
            writer.WriteLine($"\t{field.Name} <=>\n\t~{nestedObjectType.Name.Replace("Model", "")}");

            foreach (var nestedField in nestedObjectType.GetFields())
            {
                var nestedValue = nestedField.GetValue(value);
                writer.WriteLine($"\t\t{nestedField.Name} <=> {nestedValue}");
            }

            writer.WriteLine("\t---");
        }

        private void OutputNestedList(object value, StreamWriter writer, FieldInfo field)
        {
            var list = (IEnumerable<object>)value;
            writer.WriteLine($"\t{field.Name} <=> [");
            foreach (var item in list)
            {
                Type itemType = item.GetType();
                writer.WriteLine($"\t~{itemType.Name.Replace("Model", "")}");
                foreach (var itemField in itemType.GetFields())
                {
                    var itemValue = itemField.GetValue(item);
                    writer.WriteLine($"\t\t{itemField.Name} <=> {itemValue}");
                }
                writer.WriteLine("\t---");
            }
            writer.WriteLine("\t]");
        }

        private string GetLine(StreamReader reader)
        {
            string line = reader.ReadLine();
            if (line.Trim() == "")
                throw new Exception("Empty line in a file");
            return line;
        }
        
        public List<Vehicle> Deserialize(string fileName)
        {
            List<VehicleSerializableModel> models = new List<VehicleSerializableModel>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                string line = GetLine(reader);
                if (!line.Trim().Equals("[")) throw new Exception("Error in line: " + line.Trim() + "\n'[' expected");
                while (!reader.EndOfStream)
                {
                    line = GetLine(reader);
                    if (line.Trim().StartsWith("~"))
                    {
                        AssembleVehicle(out object model, reader, line);
                        VehicleSerializableModel.CheckFields(model);
                        models.Add((VehicleSerializableModel)model);
                    }
                    else
                        if (line.Trim().StartsWith("]"))
                            break;
                    else
                        throw new Exception("Error in line: " + line.Trim() + "\n'~Class' expected");
                }
                return ClassConverter.ModelsToVehicles(models);
            }
        }

        private void AssembleVehicle(out object model, StreamReader reader, string line)
        {
            Type modelType = Type.GetType("OOP_Application.Serializable_Classes." + line.Trim().Substring(1) + "SerializableModel") ?? throw new Exception("Error in line: " + line.Trim() + $"\nUnknown class '{line.Trim().Substring(1)}' ");
            model = Activator.CreateInstance(modelType);
            while (!reader.EndOfStream && !line.Trim().StartsWith("---"))
            {
                line = GetLine(reader);
                if (line.Trim().StartsWith("---")) break;
                string[] parts = line.Trim().Split(new[] { "<=>" }, StringSplitOptions.RemoveEmptyEntries);
                string propertyName = parts[0].Trim();
                FieldInfo field = modelType.GetField(propertyName) ?? throw new Exception("Error in line: " + line.Trim() + $"\nWrong field name '{parts[0].Trim()}' ");
                if (parts.Length >= 2)
                {
                    string propertyValue = parts[1].Trim();
                    if (field.FieldType.IsPrimitive || field.FieldType == typeof(string) || field.FieldType.IsEnum)
                    {
                        if (field.FieldType.IsEnum)
                        {
                            object enumValue;
                            try
                            {
                                enumValue = Enum.Parse(field.FieldType, propertyValue);
                            }
                            catch { throw new Exception("Error in line: " + line.Trim() + $"\nWrong field value '{parts[1].Trim()}' "); };
                            field.SetValue(model, enumValue);
                        }
                        else
                        {
                            TypeCode typeCode = Type.GetTypeCode(field.FieldType);
                            object convertedValue;
                            try
                            {
                                convertedValue = Convert.ChangeType(propertyValue, typeCode);
                            }
                            catch { throw new Exception("Error in line: " + line.Trim() + $"\nWrong field value '{parts[1].Trim()}' "); };
                            field.SetValue(model, convertedValue);
                        }
                    }
                    else if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        if (parts.Length > 2 || parts[parts.Length - 1].Trim() != "[") throw new Exception("Error in line: " + line.Trim() + $"\nOnly 'field <=>' expected'{line.Trim()}' ");
                        AssembleObjectsList(field, reader, out IList list);
                        field.SetValue(model, list);
                    }
                }
                else
                {
                    if (parts.Length > 1) throw new Exception("Error in line: " + line.Trim() + $"\nOnly 'field <=>' expected'{line.Trim()}' ");
                    AssembleNestedObject(field, reader, out object nestedObj);
                    field.SetValue(model, nestedObj);
                    line = "";
                }
            }
        }

        private void AssembleNestedObject(FieldInfo field, StreamReader reader, out object nestedObj)
        {
            Type nestedObjectType = field.FieldType;
            nestedObj = Activator.CreateInstance(nestedObjectType);
            string line = GetLine(reader);
            if (!line.Trim().StartsWith("~") || (line.Trim().Split(new[] { ' ' }).Length > 1) || line.Trim().IndexOf("Driver") == -1) { throw new Exception("Error in line: " + line.Trim() + "\n'~Class' expected"); }
            line = GetLine(reader);
            while (!reader.EndOfStream && !line.Trim().StartsWith("---"))
            {
                string[] nestedParts = line.Trim().Split(new[] { "<=>" }, StringSplitOptions.RemoveEmptyEntries);
                string nestedPropertyName = nestedParts[0].Trim();
                FieldInfo nestedFieldInfo = nestedObjectType.GetField(nestedPropertyName) ?? throw new Exception("Error in line: " + line.Trim() + $"\nWrong field name '{nestedParts[0].Trim()}' ");
                if (nestedParts.Length != 2) throw new Exception("Error in line: " + line.Trim() + $"\nWrong 'field <=> value' declaration '{line.Trim()}' ");
                string nestedPropertyValue = nestedParts[1].Trim();
                if (nestedFieldInfo.FieldType.IsEnum)
                {
                    object enumValue;
                    try
                    {
                        enumValue = Enum.Parse(nestedFieldInfo.FieldType, nestedPropertyValue);
                    }
                    catch { throw new Exception("Error in line: " + line.Trim() + $"\nWrong field value '{nestedParts[1].Trim()}' "); };
                    nestedFieldInfo.SetValue(nestedObj, enumValue);
                }
                else
                {
                    TypeCode nestedFieldTypeCode = Type.GetTypeCode(nestedFieldInfo.FieldType);
                    object nestedConvertedValue;
                    try
                    {
                        nestedConvertedValue = Convert.ChangeType(nestedPropertyValue, nestedFieldTypeCode);
                    }
                    catch { throw new Exception("Error in line: " + line.Trim() + $"\nWrong field value '{nestedParts[1].Trim()}' "); };
                    nestedFieldInfo.SetValue(nestedObj, nestedConvertedValue);
                }
                line = GetLine(reader);
            }
        }

        private void AssembleObjectsList(FieldInfo field, StreamReader reader, out IList list)
        {
            Type itemType = field.FieldType.GetGenericArguments()[0];
            list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));
            string line = GetLine(reader);
            if (!line.Trim().StartsWith("~") || (line.Trim().Split(new[] { ' ' }).Length > 1) || line.Trim().IndexOf("Passenger") == -1) { throw new Exception("Error in line: " + line.Trim() + "\n'~Class' expected"); }
            while (!reader.EndOfStream && !line.Trim().StartsWith("]"))
            {
                line = GetLine(reader);
                if (line.Trim().StartsWith("---")) break;
                object item = Activator.CreateInstance(itemType);
                while (!reader.EndOfStream && !line.Trim().StartsWith("---"))
                {
                    string[] itemParts = line.Trim().Split(new[] { "<=>" }, StringSplitOptions.RemoveEmptyEntries);
                    if (itemParts.Length != 2) throw new Exception("Error in line: " + line.Trim() + $"\nWrong 'field <=> value' declaration '{line.Trim()}' ");
                    string itemPropertyName = itemParts[0].Trim();
                    string itemPropertyValue = itemParts[1].Trim();
                    FieldInfo itemField = itemType.GetField(itemPropertyName) ?? throw new Exception("Error in line: " + line.Trim() + $"\nWrong field name '{itemParts[0].Trim()}' ");
                    if (itemField.FieldType.IsEnum)
                    {
                        object enumValue;
                        try
                        {
                            enumValue = Enum.Parse(itemField.FieldType, itemPropertyValue);
                        }
                        catch { throw new Exception("Error in line: " + line.Trim() + $"\nWrong field value '{itemPropertyValue}' "); };
                        itemField.SetValue(item, enumValue);
                    }
                    else
                    {
                        TypeCode itemFieldTypeCode = Type.GetTypeCode(itemField.FieldType);
                        object itemConvertedValue;
                        try
                        {
                            itemConvertedValue = Convert.ChangeType(itemPropertyValue, itemFieldTypeCode);
                        }
                        catch { throw new Exception("Error in line: " + line.Trim() + $"\nWrong field value '{itemPropertyValue}' "); };
                        itemField.SetValue(item, itemConvertedValue);
                    }
                    line = GetLine(reader);
                }
                list.Add(item);
                line = GetLine(reader);
                if (!line.Trim().StartsWith("]") && (!line.Trim().StartsWith("~") || (line.Trim().Split(new[] { ' ' }).Length > 1) || line.Trim().IndexOf("Passenger") == -1)) { throw new Exception("Error in line: " + line.Trim() + "\n'~Class' expected"); }
            }
        }
    }
}
