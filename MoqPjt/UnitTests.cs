using System;
using System.Security.Cryptography.X509Certificates;
using Moq;
using NUnit.Framework;

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
            
            _mock.Setup(x => x.GetName(It.IsAny<int>())).Returns((string s) => s);
            var mockObject = new Company(_mock.Object);
            Assert.AreSame("Nike", mockObject.GetName(2));
        }

        [Test]
        public void SendYearOfBirth_AssertAge()
        {
             _mock = new Mock<ICustomer>();
             _mock.SetupGet(x => x.CustomerName).Returns(() => "Sharp");
            _mock.Setup(x => x.GetAge(It.IsAny<int>())).Returns((int x) => x);
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
