using OOP_Application.Serializable_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Serializers
{
    public static class ClassConverter
    {
        public static DriverModel DriverToModel(Driver driver)
        {
            return new DriverModel(driver.name, driver.age, (DriverModel.Category)(driver.category + 1));
        }

        public static Driver ModelToDriver(DriverModel driver)
        {
            return new Driver(driver.name, driver.age, (Driver.Category)(driver.category - 1));
        }

        public static List<PassengerModel> PassengersToModel(List<Passenger> passengers)
        {
            var models = new List<PassengerModel>();
            foreach (var passenger in passengers)
            {
                models.Add(new PassengerModel(passenger.bagWeight, passenger.name, passenger.age));
            }
            return models;
        }

        public static List<Passenger> ModelToPassengers(List<PassengerModel> passengers)
        {
            var models = new List<Passenger>();
            foreach (var passenger in passengers)
            {
                models.Add(new Passenger(passenger.bagWeight, passenger.name, passenger.age));
            }
            return models;
        }

        public static List<Vehicle> ModelsToVehicles(List<VehicleSerializableModel> vehicleSerializableModels)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            foreach (var model in vehicleSerializableModels)
            {
                switch (model)
                {
                    case CarSerializableModel _:
                        var car = (CarSerializableModel)model;
                        vehicles.Add(new Car((Car.CarType)(car.carType - 1), car.wheelsAmount, car.horsePower, car.brand, car.price, car.year, car.seatsAmount, ModelToDriver(car.driver), ModelToPassengers(car.passengers)));
                        break;
                    case TruckSerializableModel _:
                        var truck = (TruckSerializableModel)model;
                        vehicles.Add(new Truck(truck.loadCapacity, truck.bodyVolume, truck.wheelsAmount, truck.horsePower, truck.brand, truck.price, truck.year, truck.seatsAmount, ModelToDriver(truck.driver), ModelToPassengers(truck.passengers)));
                        break;
                    case HelicopterSerializableModel _:
                        var helicopter = (HelicopterSerializableModel)model;
                        vehicles.Add(new Helicopter(helicopter.bladesAmount, helicopter.bladesLength, helicopter.flyingHeight, helicopter.brand, helicopter.price, helicopter.year, helicopter.seatsAmount, ModelToDriver(helicopter.driver), ModelToPassengers(helicopter.passengers)));
                        break;
                    case PlaneSerializableModel _:
                        var plane = (PlaneSerializableModel)model;
                        vehicles.Add(new Plane((Plane.PlaneType)(plane.planeType - 1), plane.flyingHeight, plane.brand, plane.price, plane.year, plane.seatsAmount, ModelToDriver(plane.driver), ModelToPassengers(plane.passengers)));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(model));
                }
            }
            return vehicles;
        }
        public static List<VehicleSerializableModel> VehiclesToModels(List<Vehicle> vehicles)
        {
            List<VehicleSerializableModel> models = new List<VehicleSerializableModel>();
            foreach (var vehicle in vehicles)
            {
                switch (vehicle)
                {
                    case Car _:
                        var carModel = (Car)vehicle;
                        models.Add(new CarSerializableModel((CarSerializableModel.CarType)(carModel.carType + 1), carModel.wheelsAmount, carModel.horsePower, carModel.brand, carModel.price, carModel.year, carModel.seatsAmount, DriverToModel(carModel.driver), PassengersToModel(carModel.passengers)));
                        break;
                    case Truck _:
                        var truckModel = (Truck)vehicle;
                        models.Add(new TruckSerializableModel(truckModel.brand, truckModel.price, truckModel.year, truckModel.seatsAmount, DriverToModel(truckModel.driver), PassengersToModel(truckModel.passengers), truckModel.wheelsAmount, truckModel.horsePower, truckModel.loadCapacity, truckModel.bodyVolume));
                        break;
                    case Helicopter _:
                        var helicopterModel = (Helicopter)vehicle;
                        models.Add(new HelicopterSerializableModel(helicopterModel.brand, helicopterModel.price, helicopterModel.year, helicopterModel.seatsAmount, DriverToModel(helicopterModel.driver), PassengersToModel(helicopterModel.passengers), helicopterModel.flyingHeight, helicopterModel.bladesAmount, helicopterModel.bladesLength));
                        break;
                    case Plane _:
                        var planeModel = (Plane)vehicle;
                        models.Add(new PlaneSerializableModel(planeModel.brand, planeModel.price, planeModel.year, planeModel.seatsAmount, DriverToModel(planeModel.driver), PassengersToModel(planeModel.passengers), planeModel.flyingHeight, (PlaneSerializableModel.PlaneType)(planeModel.planeType + 1)));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(vehicle));
                }
            }
            return models;
        }
    }
}
