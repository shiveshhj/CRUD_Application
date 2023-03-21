using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Factories
{
    class TruckFactory : VehicleFactory
    {
        public override Vehicle createVehicle(List<object> fields)
        {
            Driver.Category category = (Driver.Category)Enum.Parse(typeof(Driver.Category), (string)fields[10]);
            Driver driver = new Driver((string)fields[8], Convert.ToInt32(fields[9]), category);
            List<Passenger> _passengers = (List<Passenger>)fields[fields.Count - 1];
            return new Truck(
                Convert.ToInt32(fields[0]),
                Convert.ToInt32(fields[1]),
                Convert.ToInt32(fields[2]),
                Convert.ToInt32(fields[3]),
                (string)fields[4],
                Convert.ToInt32(fields[5]),
                Convert.ToInt32(fields[6]),
                Convert.ToInt32(fields[7]),
                driver,
                _passengers);
        }
    }
}
