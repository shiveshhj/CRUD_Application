using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    abstract class LandVehicle : Vehicle
    {
        protected int wheelsAmount;
        protected int horsePower;

        public LandVehicle() { }
        public LandVehicle(int _wheelsAmount, int _horsePower, string _brand, int _price, int _year, int _seatsAmount, Driver _driver, List<Passenger> _passengers) :
                      base(_brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.wheelsAmount = _wheelsAmount;
            this.horsePower = _horsePower;
        }
    }
}
