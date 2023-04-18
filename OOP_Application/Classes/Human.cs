using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    public abstract class Human
    {
        [Name("age")]
        public int age;
        [Name("name")]
        public string name;

        public Human()
        {
        }

        public Human(string _name, int _age)
        {
            this.name = _name;
            this.age = _age;
        }
    }
}
