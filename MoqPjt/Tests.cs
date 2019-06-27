using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Moq;
using MoqPjt;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using System.Linq;
using System.Threading;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

            var mock = new Mock<IFoo>();
            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);


            // out arguments
            var outString = "ack";
            // TryParse will return true, and the out argument will return "ack", lazy evaluated
            mock.Setup(foo => foo.TryParse("ping", out outString)).Returns(true);


            // ref arguments
            var instance = new Bar();
            // Only matches if the ref argument to the invocation is the same instance
            mock.Setup(foo => foo.Submit(ref instance)).Returns(true);


            // access invocation arguments when returning a value
            mock.Setup(x => x.DoSomethingStringy(It.IsAny<string>()))
                .Returns((string s) => s.ToLower());
            // Multiple parameters overloads available


            // throwing when invoked with specific parameters
            mock.Setup(foo => foo.DoSomething("reset")).Throws<InvalidOperationException>();
            mock.Setup(foo => foo.DoSomething("")).Throws(new ArgumentException("command"));


            // lazy evaluating return value
            var count = 1;
            mock.Setup(foo => foo.GetCount()).Returns(() => count);


        }

        [Test]
        public void Test_Mock()
        {
            // Arrange
            var mock = new Mock<IStrings>();
            string string1 = "Hella";
            string string2 = "World";
            mock.Setup(x => x.AMethodCall(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((s1, s2) =>
                {
                    string1 = s1;
                    string2 = s2;
                }); //.Returns((string c, string b) => c + b);

            var sut = new ServiceUnderTest(mock.Object);
            // Act
            sut.DoIt();

            // Assert
            Assert.That(string1, Is.EqualTo("Hello"));
            Assert.That(string2, Is.EqualTo("World"));

        }

        [Test]
        public void Test_Index()
        {
            Company company = new Company();
            var d = company["emp1"];
            company["emp1"] = "employee100";
        }

        [Test]
        public void Test_TimeSpan()
        {

            var ts = TimeSpan.Parse("00:01:00");
            Thread.Sleep(ts);

            string s = "00:01:00";
            var f = s.Split(":");
            var dt = new TimeSpan(00, 01, 00);
            Thread.Sleep(dt);
        }
    }

    public class Employee
    {
        public string EmployeeName;
        public int EmployeeNumber;
    }

    public class Company
    {
        private string companyName;
        private int companyNumber;
        private IList<Employee> empList;
        public Company()
        {
            empList = new List<Employee>
            {
                new Employee {EmployeeName = "emp1", EmployeeNumber = 1001},
                new Employee {EmployeeName = "emp2", EmployeeNumber = 1002}
            };
        }

        public string this[string employeeName]
        {
            get => empList.FirstOrDefault(x => x.EmployeeName == employeeName)?.EmployeeNumber.ToString();
            set => empList.FirstOrDefault(x => x.EmployeeName == value);
        }
    }
}

    public interface IStrings
    {
       void AMethodCall(string a,string b);
     
    }



public class ServiceUnderTest
{
    private IStrings _strings;

    public ServiceUnderTest(IStrings strings)
    {
        _strings = strings;
    }

    public void DoIt()
    {
        Console.WriteLine("Over");
    }
}