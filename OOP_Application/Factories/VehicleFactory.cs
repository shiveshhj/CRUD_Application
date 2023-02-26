using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Factories
{
    abstract class VehicleFactory
    {
        public abstract Vehicle createVehicle(List<string> fields, List<Passenger> _passengers);
    }
}
