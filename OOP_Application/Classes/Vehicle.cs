using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    public abstract class Vehicle
    {
        [Name("Brand")]
        public string brand;
        [Name("Price")]
        public int price;
        [Name("Year")]
        public int year;
        [Name("Seats amount")]
        public int seatsAmount;
        public Driver driver;
        public List<Passenger> passengers;


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

        public string Brand
        {
            get => brand;
        }

        public int Price
        {
            get => price;
        }

        public int Year
        {
            get => year;
        }

        public int SeatsAmount
        {
            get => seatsAmount;
        }

        public override bool Equals(object obj)
        {
            return obj is Vehicle vehicle &&
                   brand == vehicle.brand &&
                   price == vehicle.price &&
                   year == vehicle.year &&
                   seatsAmount == vehicle.seatsAmount &&
                   driver.Equals(vehicle.driver) &&
                   passengers.SequenceEqual(vehicle.passengers) &&
                   Brand == vehicle.Brand &&
                   Price == vehicle.Price &&
                   Year == vehicle.Year &&
                   SeatsAmount == vehicle.SeatsAmount;
        }
    }
}
