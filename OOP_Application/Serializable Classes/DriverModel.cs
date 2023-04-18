using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Serializable_Classes
{
    [Serializable]
    public class DriverModel
    {
        public enum Category { Invalid, A, B, C, D, F, I, PPL, FAA };
        public Category category = Category.Invalid;
        public int age = -1;
        public string name = "";

        public DriverModel()
        {
        }

        public DriverModel(string _name, int _age, Category _category)
        {
            this.name = _name;
            this.age = _age;
            this.category = _category;
        }
    }
}
