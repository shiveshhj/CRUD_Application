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
    }
}
