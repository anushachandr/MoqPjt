using System.IO;
using BoDi;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace SpecflowNunitTests
{
    [Binding]
    public  class CommonBinding
    {
        private IObjectContainer _objCon;

        public CommonBinding(IObjectContainer objCon)
        {
            _objCon = objCon;
        }
      
        [BeforeTestRun]
        public static void BeforeTestRun(IObjectContainer oc)
        {
            oc.RegisterTypeAs<WebDriverTest, IWebDriverTest>();
            var d = Directory.GetCurrentDirectory();
            oc.RegisterInstanceAs<IWebDriverTest>(new WebDriverTest(new ChromeDriver(Directory.GetCurrentDirectory())));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var d = _objCon.Resolve<IWebDriverTest>();
            d.WebDriver.Dispose();

        }
      

    }
}
