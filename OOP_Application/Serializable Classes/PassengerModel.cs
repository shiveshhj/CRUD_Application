using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Serializable_Classes
{
    [Serializable]
    public class PassengerModel
    {
        public int age = -1;
        public string name = "";
        public int bagWeight = -1;
        public PassengerModel() { }
        public PassengerModel(int _bagWeight, string _name, int _age) 
        {
            this.bagWeight = _bagWeight;
            this.name = _name;
            this.age = _age;
        }
    }
}
