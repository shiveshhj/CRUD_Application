using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    [Name("Car")]
    class Car : LandVehicle
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
    }
}
