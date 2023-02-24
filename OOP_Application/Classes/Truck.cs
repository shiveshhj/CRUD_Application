using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    class Truck : LandVehicle
    {
        private int loadCapacity;
        private int bodyVolume;

        public Truck(int _loadCapacity, int _bodyVolume, int _wheelsAmount, int _horsePower, string _brand, int _price, int _year, int _seatsAmount, Driver _driver, List<Passenger> _passengers) :
                base(_wheelsAmount, _horsePower, _brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.loadCapacity = _loadCapacity;
            this.bodyVolume = _bodyVolume;
        }
    }
}
