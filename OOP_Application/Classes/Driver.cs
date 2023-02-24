using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    class Driver : Human
    {
        [Flags]
        public enum DrivingCategory { A, B, C, D, F, I, PPL, FAA };
        private DrivingCategory drivingCategory;

        public Driver(DrivingCategory _drivingCategory, string _name, int _age) : base(_name, _age)
        {
            this.drivingCategory = _drivingCategory;
        }
    }
}
