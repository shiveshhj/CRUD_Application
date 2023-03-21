using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Factories
{
    class CarFactory : VehicleFactory
    {
        public override Vehicle createVehicle(List<object> fields)
        {
            Driver.Category category = (Driver.Category)Enum.Parse(typeof(Driver.Category),(string)fields[9]);
            Driver driver = new Driver((string)fields[7], Convert.ToInt32(fields[8]), category);
            List<Passenger> _passengers = (List<Passenger>)fields[fields.Count - 1];
            return new Car(
                (Car.CarType)Enum.Parse(typeof(Car.CarType), (string)fields[0]),
                Convert.ToInt32(fields[1]),
                Convert.ToInt32(fields[2]),
                (string)fields[3],
                Convert.ToInt32(fields[4]),
                Convert.ToInt32(fields[5]),
                Convert.ToInt32(fields[6]),
                driver,
                _passengers);
        }
    }
}
