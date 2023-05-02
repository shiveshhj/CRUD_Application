using System;
using System.Collections.Generic;

namespace OOP_Application.Serializable_Classes
{
    [Serializable]
    public class CarSerializableModel : VehicleSerializableModel
    {
        public enum CarType { Invalid, Hatchback, Sedan, Pickup, Coupe, Minivan, StationWagon };
        public CarType carType = CarType.Invalid;
        public int wheelsAmount = -1;
        public int horsePower = -1;

        public CarSerializableModel() { }
        public CarSerializableModel(CarType _carType, int _wheelsAmount, int _horsePower, string _brand, int _price, int _year, int _seatsAmount, DriverModel _driver, List<PassengerModel> _passengers) :
              base(_brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.carType = _carType;
            this.wheelsAmount = _wheelsAmount;
            this.horsePower = _horsePower;
        }
    }
}
