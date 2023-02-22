using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    class Plane : AirVehicle
    {
        public enum PlaneType { Passenger, Cargo, Millitary };
        private PlaneType planeType;
    }
}
