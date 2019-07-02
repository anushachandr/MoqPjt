using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoDi;
using TechTalk.SpecFlow;

namespace SpecflowNunitTests
{
    [Binding]
    public class Feauture1Binding
    {
        
        public Feauture1Binding(IObjectContainer oc)
        {
            var d = oc.Resolve<IWebDriverTest>().WebDriver;
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {

        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
      
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
          
        }
    }
}
