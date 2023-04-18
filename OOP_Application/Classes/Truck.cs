using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    [Name("Truck")]
    public class Truck : LandVehicle
    {
        [Name("Load capacity")]
        public int loadCapacity;
        [Name("Body volume")]
        public int bodyVolume;

        public Truck() { }
        public Truck(int _loadCapacity, int _bodyVolume, int _wheelsAmount, int _horsePower, string _brand, int _price, int _year, int _seatsAmount, Driver _driver, List<Passenger> _passengers) :
                base(_wheelsAmount, _horsePower, _brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.loadCapacity = _loadCapacity;
            this.bodyVolume = _bodyVolume;
        }

        public override bool Equals(object obj)
        {
            return obj is Truck truck &&
                   brand == truck.brand &&
                   price == truck.price &&
                   year == truck.year &&
                   seatsAmount == truck.seatsAmount &&
                   driver.Equals(truck.driver) &&
                   passengers.SequenceEqual(truck.passengers) &&
                   Brand == truck.Brand &&
                   Price == truck.Price &&
                   Year == truck.Year &&
                   SeatsAmount == truck.SeatsAmount &&
                   wheelsAmount == truck.wheelsAmount &&
                   horsePower == truck.horsePower &&
                   loadCapacity == truck.loadCapacity &&
                   bodyVolume == truck.bodyVolume;
        }
    }
}
