using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    public class Driver : Human
    {
        public enum Category { A, B, C, D, F, I, PPL, FAA };
        [Name("category")]
        public Category category;

        public Driver()
        {
        }

        public Driver(string _name, int _age, Category _category) : base(_name, _age)
        {
            this.category = _category;
        }

        public override bool Equals(object obj)
        {
            return obj is Driver driver &&
                   age == driver.age &&
                   name == driver.name &&
                   category == driver.category;
        }

    }
}
