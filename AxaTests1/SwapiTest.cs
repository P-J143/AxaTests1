using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace AxaTests1
{

    public class SwapiTest
    {
        private const string HomeUrl = "https://swapi.dev/";
        private const string HomeTitle = "SWAPI - The Star Wars API";
        private const string LukeName = "Luke Skywalker";
        private const string LukeHomeworld = "Tatooine";
        private const string Planet1 = "https://swapi.dev/api/planets/1/";



        [Fact]
        public void TestIfLuke()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                driver.FindElement(By.ClassName("form-control")).Clear(); //clear the text box
                driver.FindElement(By.ClassName("form-control")).SendKeys("people/1");
                Assert.Contains(LukeName, driver.FindElement(By.ClassName("well")).Text); // confirm that test is looking at Luke Skywalker
                Assert.Contains(Planet1, driver.FindElement(By.ClassName("well")).Text); // confirm that homeworld = planet1
                Assert.Contains("https://swapi.dev/api/people/1/", driver.FindElement(By.ClassName("well")).Text); // confirm that people/1 = Luke
                driver.FindElement(By.ClassName("form-control")).Clear(); //clear the text box
                driver.FindElement(By.ClassName("form-control")).SendKeys("planets/1");
                driver.FindElement(By.ClassName("input-group-btn")).Click(); // go to the homeworld
                var Results = driver.FindElement(By.ClassName("well"));
                wait.Until(ExpectedConditions.TextToBePresentInElement(Results, LukeHomeworld)); // wait for Tatooine to show up
                Assert.Contains(Planet1, driver.FindElement(By.ClassName("well")).Text); // planet1 = Tatooine
                Assert.Contains("https://swapi.dev/api/people/1/", driver.FindElement(By.ClassName("well")).Text); // residents contain people1(Luke)
            }

        }
    }

}
