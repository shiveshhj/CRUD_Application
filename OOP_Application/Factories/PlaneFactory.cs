using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Factories
{
    class PlaneFactory : VehicleFactory
    {
        public override Vehicle createVehicle(List<string> fields, List<Passenger> _passengers)
        {
            Driver.Category category = (Driver.Category)Enum.Parse(typeof(Driver.Category), fields[8]);
            Driver driver = new Driver(fields[6], Int32.Parse(fields[7]), category);
            return new Plane(
                (Plane.PlaneType)Enum.Parse(typeof(Plane.PlaneType), fields[0]),
                Int32.Parse(fields[1]),
                fields[2],
                Int32.Parse(fields[3]),
                Int32.Parse(fields[4]),
                Int32.Parse(fields[5]),
                driver,
                _passengers);
        }
    }
}
