abstract class Vehicle
{
    protected double price;
    protected int year;
    protected int seatsAmount;
    protected Driver driver;
    protected List<Passenger> passengers;
}

abstract class LandVehicle : Vehicle
{
    protected int wheelsAmount;
}

class Car : LandVehicle
{
    public enum CarType { Hatchback, Sedan, Pickup, Coupe, Minivan, StationWagon };
    private CarType carType;
    private int horsePower;
}

class Track : LandVehicle
{
    private int loadCapacity;
    private int bodyVolume;
}

abstract class AirVehicle : Vehicle
{
    protected int flyingHeight;
}

class Plane : AirVehicle
{
    public enum PlaneType { Passenger, Cargo, Millitary };
    private PlaneType planeType;
    private Color color;
}

class Helicopter : AirVehicle
{
    private int bladesAmount;
    private int bladesLength;
}

abstract class Human
{
    protected string name;
    protected int age;
    public enum Gender {Male, Female};
    protected Gender gender;
}


class Driver : Human
{
    public enum DrivingCategory { A, B, C, D, F, I, PPL, FAA};
    private List<DrivingCategory> drivingCategories;
}

class Passenger : Human
{
      private int bagWeight;
}