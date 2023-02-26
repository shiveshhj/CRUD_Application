using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    public class Driver : Human
    {
        [Flags]
        public enum Category { A, B, C, D, F, I, PPL, FAA };
        public Category category;

        public Driver(string _name, int _age, Category _category) : base(_name, _age)
        {
            this.category = _category;
        }
    }
}
