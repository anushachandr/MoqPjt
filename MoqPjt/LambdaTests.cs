using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using NUnit.Framework;

namespace MoqPjt
{
    public class LambdaTests
    {
        [Test]
        public void ActionTest()
        {
            Action<int> countdown = null;
            countdown = i =>
            {
                if (i > 0)
                {
                    Console.WriteLine(i);
                    countdown(--i);
                }
            };
            countdown(5);
        }

        [Test]
        public void FuncTest1()
        {
            Func<string, Dog> func = (string f) => new Dog("TestDogFunc");
            Dog instanceDog = func.Invoke("TestDog");
            instanceDog.SetLambda();

        }

        [Test]
        public void FuncTest2()
        {
            Dog d=new Dog("TestDog");
            d.SetLambda();
        }
    }

    public class Dog
    {
        string Name { get; set; }

        public Dog(string name)
        {
            Name = name;
        }

        private Action _myLambda;
        

        public void SetLambda()
        {
            _myLambda = () => Console.WriteLine("Dog is {0}", Name);
        }

        public void InvokeLambda()
        {
            _myLambda();
        }

        public string InvokeFunc(string name)
        {
            if (name == "TestDog")
                return "Woof";
            return "Not Woof";
        }
    }
}

