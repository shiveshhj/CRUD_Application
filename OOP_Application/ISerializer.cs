using OOP_Application.Serializable_Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    public interface ISerializer
    {
        void Serialize(List<Vehicle> vehicles, Stream fileStream);
        List<Vehicle> Deserialize(Stream fileStream);
    }
}
