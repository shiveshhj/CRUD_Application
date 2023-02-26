using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Factories
{
    class CarFactory : VehicleFactory
    {
        public override Vehicle createVehicle(List<string> fields, List<Passenger> _passengers)
        {
            Driver.Category category = (Driver.Category)Enum.Parse(typeof(Driver.Category), fields[9]);
            Driver driver = new Driver(fields[7], Int32.Parse(fields[8]), category);
            return new Car(
                (Car.CarType)Enum.Parse(typeof(Car.CarType), fields[0]),
                Int32.Parse(fields[1]),
                Int32.Parse(fields[2]),
                fields[3],
                Int32.Parse(fields[4]),
                Int32.Parse(fields[5]),
                Int32.Parse(fields[6]),
                driver,
                _passengers);
        }
    }
}
