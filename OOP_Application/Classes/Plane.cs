using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    [Name("Plane")]
    public class Plane : AirVehicle
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

        public override bool Equals(object obj)
        {
            return obj is Plane plane &&
                   brand == plane.brand &&
                   price == plane.price &&
                   year == plane.year &&
                   seatsAmount == plane.seatsAmount &&
                   driver.Equals(plane.driver) &&
                   passengers.SequenceEqual(plane.passengers) &&
                   Brand == plane.Brand &&
                   Price == plane.Price &&
                   Year == plane.Year &&
                   SeatsAmount == plane.SeatsAmount &&
                   flyingHeight == plane.flyingHeight &&
                   planeType == plane.planeType;
        }
    }
}
