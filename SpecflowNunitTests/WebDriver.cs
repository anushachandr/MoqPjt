using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace SpecflowNunitTests
{
    public class WebDriverTest :IWebDriverTest
    {
        public IWebDriver WebDriver { get; set; }
    }
}
