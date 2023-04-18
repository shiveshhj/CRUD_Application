using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Serializable_Classes
{
    [Serializable]
    public class TruckSerializableModel : VehicleSerializableModel
    {
        public int wheelsAmount = -1;
        public int horsePower = -1;
        public int loadCapacity = -1;
        public int bodyVolume = -1;
        public TruckSerializableModel() { }

        public TruckSerializableModel(string _brand, int _price, int _year, int _seatsAmount, DriverModel _driver, List<PassengerModel> _passengers, int _wheelsAmount, int _horsePower, int _loadCapacity, int _bodyVolume) :
            base(_brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.wheelsAmount = _wheelsAmount;
            this.horsePower = _horsePower;
            this.loadCapacity = _loadCapacity;
            this.bodyVolume = _bodyVolume;
        }
    }
}
