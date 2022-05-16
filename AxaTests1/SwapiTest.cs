using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Firefox;

namespace AxaTests1
{

    public class SwapiTests
    {
        private const string HomeUrl = "https://swapi.dev/";
        private const string HomeTitle = "SWAPI - The Star Wars API";
        private const string LukeName = "Luke Skywalker";
        private const string LukeHomeworld = "Tatooine";
        private const string Planet1 = "https://swapi.dev/api/planets/1/";


        [Fact]
        public void ChromeTestIfLuke()
        {

            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                driver.FindElement(By.XPath("/html/body/div/div[3]/div[2]/small/a[1]")).Click();
                Assert.Contains(LukeName, driver.FindElement(By.ClassName("well")).Text); // confirm that test is looking at Luke Skywalker
                Assert.Contains(Planet1, driver.FindElement(By.ClassName("well")).Text); // confirm that homeworld = planet1
                Assert.Contains("https://swapi.dev/api/people/1/", driver.FindElement(By.ClassName("well")).Text); // confirm that people/1 = Luke
                driver.FindElement(By.XPath("/html/body/div/div[3]/div[2]/div[1]/input")).Clear(); //clear the text box
                driver.FindElement(By.XPath("/html/body/div/div[3]/div[2]/div[1]/input")).SendKeys("planets/1");
                driver.FindElement(By.XPath("/html/body/div/div[3]/div[2]/div[1]/span[2]")).Click(); // go to the homeworld
                var Results = driver.FindElement(By.ClassName("well"));
                wait.Until(ExpectedConditions.TextToBePresentInElement(Results, LukeHomeworld)); // wait for Tatooine to show up
                Assert.Contains(Planet1, driver.FindElement(By.ClassName("well")).Text); // planet1 = Tatooine
                Assert.Contains("https://swapi.dev/api/people/1/", driver.FindElement(By.ClassName("well")).Text); // residents contain people1(Luke)
            }
        }

        [Fact]
        public void FirefoxTestIfLuke()
        {

            using (IWebDriver driver = new FirefoxDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                driver.FindElement(By.XPath("/html/body/div/div[3]/div[2]/small/a[1]")).Click();
                Assert.Contains(LukeName, driver.FindElement(By.ClassName("well")).Text); // confirm that test is looking at Luke Skywalker
                Assert.Contains(Planet1, driver.FindElement(By.ClassName("well")).Text); // confirm that homeworld = planet1
                Assert.Contains("https://swapi.dev/api/people/1/", driver.FindElement(By.ClassName("well")).Text); // confirm that people/1 = Luke
                driver.FindElement(By.XPath("/html/body/div/div[3]/div[2]/div[1]/input")).Clear(); //clear the text box
                driver.FindElement(By.XPath("/html/body/div/div[3]/div[2]/div[1]/input")).SendKeys("planets/1");
                driver.FindElement(By.XPath("/html/body/div/div[3]/div[2]/div[1]/span[2]")).Click(); // go to the homeworld
                var Results = driver.FindElement(By.ClassName("well"));
                wait.Until(ExpectedConditions.TextToBePresentInElement(Results, LukeHomeworld)); // wait for Tatooine to show up
                Assert.Contains(Planet1, driver.FindElement(By.ClassName("well")).Text); // planet1 = Tatooine
                Assert.Contains("https://swapi.dev/api/people/1/", driver.FindElement(By.ClassName("well")).Text); // residents contain people1(Luke)
            }
        }

    }
}
