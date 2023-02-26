using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    public abstract class Human
    {
        public string name;
        public int age;
        public Human(string _name, int _age)
        {
            this.name = _name;
            this.age = _age;
        }
    }
}
