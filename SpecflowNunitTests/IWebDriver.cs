using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace SpecflowNunitTests
{
    public interface IWebDriverTest
    {
         IWebDriver WebDriver { get; set; }
    }
}
