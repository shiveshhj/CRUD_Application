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
        [Name("Plane type")]
        public PlaneType planeType;

        public Plane() { }
        public Plane(PlaneType _planeType, int _flyingHeight, string _brand, int _price, int _year, int _seatsAmount, Driver _driver, List<Passenger> _passengers) :
                base(_flyingHeight, _brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.planeType = _planeType;
        }
    }
}
