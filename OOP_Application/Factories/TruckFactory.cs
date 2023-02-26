using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Factories
{
    class TruckFactory : VehicleFactory
    {
        public override Vehicle createVehicle(List<string> fields, List<Passenger> _passengers)
        {
            Driver.Category category = (Driver.Category)Enum.Parse(typeof(Driver.Category), fields[10]);
            Driver driver = new Driver(fields[8], Int32.Parse(fields[9]), category);
            return new Truck(
                Int32.Parse(fields[0]),
                Int32.Parse(fields[1]),
                Int32.Parse(fields[2]),
                Int32.Parse(fields[3]),
                fields[4],
                Int32.Parse(fields[5]),
                Int32.Parse(fields[6]),
                Int32.Parse(fields[7]),
                driver,
                _passengers);
        }
    }
}
