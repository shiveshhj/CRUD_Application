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
        public enum DrivingCategory { A = 1, B = 2, C = 4, D = 8, F = 16, I = 32, PPL = 64, FAA = 128 };
        private DrivingCategory drivingCategories;
    }
}
