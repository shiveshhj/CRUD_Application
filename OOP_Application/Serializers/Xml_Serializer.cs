using OOP_Application.Serializable_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace OOP_Application.Serializers
{
    public class Xml_Serializer : ISerializer
    {
        public List<Vehicle> Deserialize(Stream fileStream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<VehicleSerializableModel>));
            TextReader reader = new StreamReader(fileStream);
            return ClassConverter.ModelsToVehicles((List<VehicleSerializableModel>)serializer.Deserialize(reader));
        }

        public void Serialize(List<Vehicle> vehicles, Stream fileStream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<VehicleSerializableModel>));
            TextWriter writer = new StreamWriter(fileStream);
            serializer.Serialize(writer, ClassConverter.VehiclesToModels(vehicles));
        }
 
    }
}
