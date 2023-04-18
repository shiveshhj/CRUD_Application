using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OOP_Application.Serializable_Classes
{
    [Serializable]
    [XmlInclude(typeof(CarSerializableModel))]
    [XmlInclude(typeof(TruckSerializableModel))]
    [XmlInclude(typeof(PlaneSerializableModel))]
    [XmlInclude(typeof(HelicopterSerializableModel))]
    public abstract class VehicleSerializableModel
    {
        public string brand = "";
        public int price = -1;
        public int year = -1;
        public int seatsAmount = -1;
        public DriverModel driver;
        public List<PassengerModel> passengers;


        public VehicleSerializableModel() { }
        public VehicleSerializableModel(string _brand, int _price, int _year, int _seatsAmount, DriverModel _driver, List<PassengerModel> _passengers)
        {
            this.brand = _brand;
            this.price = _price;
            this.year = _year;
            this.seatsAmount = _seatsAmount;
            this.driver = _driver;
            this.passengers = _passengers;
        }

        public static void CheckFields(object obj)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType == typeof(string))
                {
                    if ((string)field.GetValue(obj) == "")
                        throw new Exception($"Field '{field.Name}' isn't fulfilled");
                    string value = (string)field.GetValue(obj);
                    if (value.Length > 16)
                        throw new Exception($"Field '{field.Name}' has value \"{value}\" that was longer than 16 symbols");
                    string pattern = "^[A-Za-z\\s]*$";
                    if (!Regex.IsMatch(value, pattern))
                        throw new Exception($"Field '{field.Name}' has value \"{value}\" with invalid symbols (only english letters and whitespaces are allowed)");
                }
                else if (field.FieldType == typeof(int))
                {
                    if ((int)field.GetValue(obj) == -1)
                        throw new Exception($"Field '{field.Name}' isn't fulfilled");
                    int value = (int)field.GetValue(obj);
                    if (value < 0 || value > Int32.MaxValue)
                        throw new Exception($"Field '{field.Name}' value is {value} < 0");
                }
                else if (field.FieldType.IsEnum)
                {
                    if ((int)field.GetValue(obj) == 0)
                        throw new Exception($"Field '{field.Name}' isn't fulfilled");

                }
                else if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    if (field.GetValue(obj) == null)
                        throw new Exception($"Field '{field.Name}' isn't fulfilled");
                    foreach (var passenger in (List<PassengerModel>)field.GetValue(obj))
                        CheckFields(passenger);
                }
                else
                {
                    if (field.GetValue(obj) == null)
                        throw new Exception($"Field '{field.Name}' isn't fulfilled");
                    CheckFields(field.GetValue(obj));
                }
            }
        }

    }
}
