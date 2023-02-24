using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    abstract class Vehicle
    {
        protected string brand;
        protected int price;
        protected int year;
        protected int seatsAmount;
        protected Driver driver;
        protected List<Passenger> passengers;


        public Vehicle() { }
        public Vehicle(string _brand, int _price, int _year, int _seatsAmount, Driver _driver, List<Passenger> _passengers)
        {
            this.brand = _brand;
            this.price = _price;
            this.year = _year;
            this.seatsAmount = _seatsAmount;
            this.driver = _driver;
            this.passengers = _passengers;
        }
    }
}
