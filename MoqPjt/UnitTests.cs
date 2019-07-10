using System;
using System.Security.Cryptography.X509Certificates;
using Moq;
using NUnit.Framework;
using Tests;

namespace MoqPjt
{

    public class UnitTests
    {
        private Mock<ICustomer> _mock;
        [SetUp]
        public void Setup()
        {
             _mock = new Mock<ICustomer>();
        }

        [Test]
        public void SendID_AssertName()
        { 
            Employee emp=new Employee()
            {
                EmployeeName = "Chris",
                EmployeeNumber = 1001
            };
                _mock.Setup(x => x.GetName(It.IsAny<int>()))
                    .Callback((int e)=>e=emp.EmployeeNumber)
                    .Returns((string s) => s);
            var mockObject = new Company(_mock.Object);
            Assert.AreSame("Nike", mockObject.GetName(emp.EmployeeNumber));
        }
        [Test]
        public void StubTest()
        {
            var _mock = new Mock<ICustomer<Stub>>();
            _mock.Verify(x => x.GetAge(Convert.ToInt32(It.IsAny<Stub>())));
            var f = _mock.Object;

        }

        [Test]
        public void SendYearOfBirth_AssertAge()
        {
             _mock = new Mock<ICustomer>();
             int changedAge = int.MinValue;
            // _mock.SetupGet(x => x.CustomerName).Returns(() => "Sharp");
             _mock.Setup(x => x
                     .GetAge(It.IsAny<int>()))
                 .Callback<int>(a=> { changedAge = a + 10; })
                 //.Callback((int a) =>
                 //{
                 //    Console.WriteLine($"Before change {a}");
                 //    changedAge = a + 10;
                 //})
                 .Returns<int>(a =>
                 {
                     Console.WriteLine(a);
                     return changedAge;
                 });

                 //.Returns((int x) =>
                 //{
                 //    Console.WriteLine($"Value of x is {x}");
                 //    return changedAge;
                 //});

             var mockObject = new Company(_mock.Object);
            
            Assert.AreEqual(19, mockObject.GetAge(2000));
        }
    }
    public interface ICustomer
    {
        int CustomerId { get; set; }
        string CustomerName { get; set; }
        string GetName(int id);
         int YearOfBirth { get; set; }
        int GetAge(int yearOfBirth);
    }

    public interface ICustomer<T>
    {
        int GetAge(int yearOfBirth);
    }

    public class Stub
    {

    }

   
    public class Company
    {
        private string _customerName;
        public Company(ICustomer customer)
        {
            _customerName = customer.CustomerName;
        }

        public string GetName(int cusId)
        {
            return _customerName = cusId == 2 ? "Nike" : "Swarm";
        }

        public int GetAge(int yearOfBirth)
        {
            int currentYear =Convert.ToInt16(DateTime.Now.Year.ToString());
            return currentYear - yearOfBirth;
        }
    }
}
