using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    [Name("Helicopter")]
    public class Helicopter : AirVehicle
    {
        [Name("Blades amount")]
        public int bladesAmount;
        [Name("Blades length")]
        public int bladesLength;

        public Helicopter() { }
        public Helicopter(int _bladesAmount, int _bladesLength, int _flyingHeight, string _brand, int _price, int _year, int _seatsAmount, Driver _driver, List<Passenger> _passengers) :
                     base(_flyingHeight, _brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.bladesAmount = _bladesAmount;
            this.bladesLength= _bladesLength;
        }

        public override bool Equals(object obj)
        {
            return obj is Helicopter helicopter &&
                   brand == helicopter.brand &&
                   price == helicopter.price &&
                   year == helicopter.year &&
                   seatsAmount == helicopter.seatsAmount &&
                   driver.Equals(helicopter.driver) &&
                   passengers.SequenceEqual(helicopter.passengers) &&
                   Brand == helicopter.Brand &&
                   Price == helicopter.Price &&
                   Year == helicopter.Year &&
                   SeatsAmount == helicopter.SeatsAmount &&
                   flyingHeight == helicopter.flyingHeight &&
                   bladesAmount == helicopter.bladesAmount &&
                   bladesLength == helicopter.bladesLength;
        }
    }
}
