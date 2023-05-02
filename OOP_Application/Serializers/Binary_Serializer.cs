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
        public List<Vehicle> Deserialize(Stream fileStream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return ClassConverter.ModelsToVehicles((List<VehicleSerializableModel>)formatter.Deserialize(fileStream));
        }

        public void Serialize(List<Vehicle> vehicles, Stream fileStream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, ClassConverter.VehiclesToModels(vehicles));
        }

    }
}
