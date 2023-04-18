using OOP_Application.Serializable_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    public interface ISerializer
    {
        void Serialize(List<Vehicle> vehicles, string fileName);
        List<Vehicle> Deserialize(string fileName);
    }
}
