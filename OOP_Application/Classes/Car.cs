using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    [Name("Car")]
    public class Car : LandVehicle
    {
        public enum CarType { Hatchback, Sedan, Pickup, Coupe, Minivan, StationWagon };
        [Name("Car type")]
        public CarType carType;


        public Car() { }
        public Car(CarType _carType, int _wheelsAmount, int _horsePower, string _brand, int _price, int _year, int _seatsAmount, Driver _driver, List<Passenger> _passengers) :
              base(_wheelsAmount, _horsePower, _brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.carType = _carType;
        }

        public override bool Equals(object obj)
        {
            return obj is Car car &&
                   brand == car.brand &&
                   price == car.price &&
                   year == car.year &&
                   seatsAmount == car.seatsAmount &&
                   driver.Equals(car.driver) &&
                   passengers.SequenceEqual(car.passengers) &&
                   Brand == car.Brand &&
                   Price == car.Price &&
                   Year == car.Year &&
                   SeatsAmount == car.SeatsAmount &&
                   wheelsAmount == car.wheelsAmount &&
                   horsePower == car.horsePower &&
                   carType == car.carType;
        }
    }
}
