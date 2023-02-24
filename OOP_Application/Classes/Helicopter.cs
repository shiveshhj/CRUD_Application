using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    class Helicopter : AirVehicle
    {
        private int bladesAmount;
        private int bladesLength;
        public Helicopter(int _bladesAmount, int _bladesLength, int _flyingHeight, string _brand, int _price, int _year, int _seatsAmount, Driver _driver, List<Passenger> _passengers) :
                     base(_flyingHeight, _brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.bladesAmount = _bladesAmount;
            this.bladesLength= _bladesLength;
        }
    }
}
