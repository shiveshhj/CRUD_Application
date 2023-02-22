using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    class Car : LandVehicle
    {
        public enum CarType { Hatchback, Sedan, Pickup, Coupe, Minivan, StationWagon };
        private CarType carType;
    }
}
