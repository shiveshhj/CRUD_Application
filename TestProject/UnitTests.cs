using NUnit.Framework;
using System.Collections.Generic;
using System;
using OOP_Application.Factories;
using OOP_Application;
using System.CodeDom.Compiler;
using System.Resources;

namespace TestProject
{
    public class Tests
    {
        ISerializer serializer;
        List<Vehicle> vehicles;
        [SetUp]
        public void Setup()
        {
            SerializerFactory serializerFactory = (SerializerFactory)Activator.CreateInstance(typeof(TextFactory));
            serializer = serializerFactory.CreateSerializer();

        }

        [Test]
        public void Test1()
        {
            try
            {
                vehicles = serializer.Deserialize("Tests\\test1.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Assert.That(vehicles, Is.InstanceOf<List<Vehicle>>());
            Assert.That(vehicles, Is.Not.Null);
            Assert.That(vehicles.Count, Is.EqualTo(1));
            List<Vehicle> expected = new List<Vehicle>
            {
                new Truck(0, 0, 0, 0, "brand", 0, 0, 0, new Driver("name", 0, Driver.Category.A), new List<Passenger>() { new Passenger(0, "name", 0) })
            };
            Assert.That(vehicles, Is.EqualTo(expected));
        }

        [Test]
        public void Test2()
        {
            try
            {
                vehicles = serializer.Deserialize("Tests\\test2.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Assert.That(vehicles, Is.InstanceOf<List<Vehicle>>());
            Assert.That(vehicles, Is.Not.Null);
            Assert.That(vehicles.Count, Is.EqualTo(2));
            List<Vehicle> expected = new List<Vehicle>
            {
                new Car(Car.CarType.Coupe, 4, 1000, "brrr", 500, 2022, 5, new Driver("Johny", 35, Driver.Category.B), new List<Passenger>() { new Passenger(20, "Bob", 20) }),
                new Plane(Plane.PlaneType.Passenger, 2000, "scrr", 1500, 2020, 50, new Driver("Bill", 30, Driver.Category.F), new List<Passenger>() { new Passenger(22, "Alice", 21), new Passenger(255, "Cob", 21) })
            };
            Assert.That(vehicles, Is.EqualTo(expected));
        }

        [Test]
        public void Test3()
        {
            try
            {
                vehicles = serializer.Deserialize("Tests\\test3.txt");
            }
            catch (Exception ex)
            {
                Assert.Pass(ex.Message);
            }
            Assert.Fail();
        }

        [Test]
        public void Test4()
        {
            try
            {
                vehicles = serializer.Deserialize("Tests\\test4.txt");
            }
            catch (Exception ex)
            {
                Assert.Pass(ex.Message);
            }
            Assert.Fail();
        }

        [Test]
        public void Test5()
        {
            try
            {
                vehicles = serializer.Deserialize("Tests\\test5.txt");
            }
            catch (Exception ex)
            {
                Assert.Pass(ex.Message);
            }
            Assert.Fail();
        }
    }
}