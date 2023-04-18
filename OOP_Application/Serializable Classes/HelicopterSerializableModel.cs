using System;
using System.Collections.Generic;

namespace OOP_Application.Serializable_Classes
{
    [Serializable]
    public class HelicopterSerializableModel : VehicleSerializableModel
    {
        public int flyingHeight = -1;
        public int bladesAmount = -1;
        public int bladesLength = -1;
        public HelicopterSerializableModel() { }

        public HelicopterSerializableModel(string _brand, int _price, int _year, int _seatsAmount, DriverModel _driver, List<PassengerModel> _passengers, int _flyingHeight, int _bladesAmount, int _bladesLength) :
            base(_brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.flyingHeight = _flyingHeight;
            this.bladesAmount = _bladesAmount;
            this.bladesLength = _bladesLength;
        }
    }
}
