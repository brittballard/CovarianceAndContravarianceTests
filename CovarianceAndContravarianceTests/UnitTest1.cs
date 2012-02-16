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
            var func1 = new Func<Person, Person>(p => { p.Name = "x"; return p; });
            var func2 = new Func<Employee, Person>(e => { e.Name = "y"; return e; });

            func2 = func1;

            //This errors.
            //var person = func2(new Person { Age = 32 });
            var person = func2(new Employee { Age = 32 });
            Assert.AreEqual(person.Name, "x");
        }

        [TestMethod]
        public void Func_Generics_ReturnType_Covariance()
        {
            var func1 = new Func<Person, Person>(p => { p.Name = "britton"; return p; });
            var func2 = new Func<Person, Animal>(p => { p.Name = "cassie"; return p; });

            func2 = func1;

            //This errors.
            //Person returnValue = func2(new Employee { Age = 32 });
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
