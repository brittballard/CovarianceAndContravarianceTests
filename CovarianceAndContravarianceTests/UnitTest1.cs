using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CovarianceAndContravarianceTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Func_Generics_Parameter_Contravariance()
        {
            var func = new Func<Person, Person>(p => p);
            
            // paramatere contravariant: from person -> employee (from "narrower" -> to "wider")
            Person animal = func(new Employee { Age = 32 });
            Assert.IsInstanceOfType(animal, typeof(Employee));
        }

        [TestMethod]
        public void Func_Generics_ReturnType_Covariance()
        {
            var func1 = new Func<Person, Person>(p => { p.Name = "britton"; return p; });
            var func2 = new Func<Person, Animal>(p => { p.Name = "cassie"; return p; });

            func2 = func1;

            // paramatere contravariant: from person -> employee (from "narrower" -> to "wider")
            Animal returnValue = func2(new Employee { Age = 32 });
            Assert.IsInstanceOfType(returnValue, typeof(Person));
            Assert.AreEqual("britton", ((Person)returnValue).Name);
        }
    }

    public class Animal
    {
        public int Age { get; set; }
    }

    public class Person : Animal
    {
        public string Name { get; set; }
    }

    public class Employee : Person
    {
    }
}
