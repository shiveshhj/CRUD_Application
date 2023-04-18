using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OOP_Application.Plane;

namespace OOP_Application.Serializable_Classes
{
    [Serializable]
    public class PlaneSerializableModel : VehicleSerializableModel
    {
        public int flyingHeight = -1;
        public enum PlaneType { Invalid, Passenger, Cargo, Millitary };

        public PlaneType planeType = PlaneType.Invalid;
        public PlaneSerializableModel() { }

        public PlaneSerializableModel(string _brand, int _price, int _year, int _seatsAmount, DriverModel _driver, List<PassengerModel> _passengers, int _flyingHeight, PlaneType _planeType) :
              base(_brand, _price, _year, _seatsAmount, _driver, _passengers)
        {
            this.flyingHeight = _flyingHeight;
            this.planeType = _planeType;
        }
    }
}
