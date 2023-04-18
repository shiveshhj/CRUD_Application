using OOP_Application.Serializable_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Serializers
{
    internal class Binary_Serializer : ISerializer
    {
        public List<Vehicle> Deserialize(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                return ClassConverter.ModelsToVehicles((List<VehicleSerializableModel>)formatter.Deserialize(fs));
            }
        }

        public void Serialize(List<Vehicle> vehicles, string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, ClassConverter.VehiclesToModels(vehicles));
            }
        }

    }
}
