using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Factories
{
    class PlaneFactory : VehicleFactory
    {
        public override Vehicle createVehicle(List<object> fields)
        {
            Driver.Category category = (Driver.Category)Enum.Parse(typeof(Driver.Category), (string)fields[8]);
            Driver driver = new Driver((string)fields[6], Convert.ToInt32(fields[7]), category);
            List<Passenger> _passengers = (List<Passenger>)fields[fields.Count - 1];
            return new Plane(
                (Plane.PlaneType)Enum.Parse(typeof(Plane.PlaneType), (string)fields[0]),
                Convert.ToInt32(fields[1]),
                (string)fields[2],
                Convert.ToInt32(fields[3]),
                Convert.ToInt32(fields[4]),
                Convert.ToInt32(fields[5]),
                driver,
                _passengers);
        }
    }
}
