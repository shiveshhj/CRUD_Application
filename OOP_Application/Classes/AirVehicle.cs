using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    abstract class AirVehicle : Vehicle
    {
        protected int flyingHeight;
        public AirVehicle(int _flyingHeight, string _brand, int _price, int _year, int _seatsAmount, Driver _driver, List<Passenger> _passengers) :
                      base(_brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.flyingHeight = _flyingHeight;
        }
    }
}
