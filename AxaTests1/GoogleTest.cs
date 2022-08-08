using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using System.Linq;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace AxaTests1
{

    public class GoogleTest
    {
        private const string HomeUrl = "https://www.google.pl/maps/";
        private const string HomeTitle = "Mapy Google";
        private const int WalkTime = 40;
        private const double Distance = 30;
        private const int BicycleTime = 15;

        [Fact]
        public void OfficeJourney()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                driver.Manage().Window.Maximize();

                driver.Navigate().GoToUrl(HomeUrl);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                var decimalSeparator = ',';

                // Decline cookies
                var cookiesbutton1 = driver.FindElements(By.CssSelector(".VfPpkd-vQzf8d"));
                var cookiesbutton2 = cookiesbutton1.FirstOrDefault(e => e.Text.Contains("Odrzuć wszystko"));
                cookiesbutton2.Click();

                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#searchbox")));
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                // Enter the address, search for the on foot route TO the office
                driver.FindElement(By.Id("searchboxinput")).SendKeys("Chłodna 51 Warszawa");
                driver.FindElement(By.ClassName("hArJGc")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#sb_ifc51 > input")));
                driver.FindElement(By.CssSelector("#sb_ifc51 > input")).SendKeys("Plac Defilad 1 Warszawa");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[aria-label='Pieszo']")));
                driver.FindElement(By.CssSelector("[aria-label='Pieszo']")).Click();

                wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("d1qxPd")));
                var TextWalkTimeToOffice = driver.FindElement(By.CssSelector(".Fk3sm.fontHeadlineSmall")).Text;
                var TextWalkDistanceToOffice = driver.FindElement(By.CssSelector(".ivN21e.tUEI8e.fontBodyMedium")).Text;

                var ResultWalkTimeToOffice = new string(TextWalkTimeToOffice.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultWalkDistanceToOffice = new string(TextWalkDistanceToOffice.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                Double.TryParse(ResultWalkTimeToOffice, out double NumWalkTimeToOffice);
                Double.TryParse(ResultWalkDistanceToOffice, out double NumWalkDistanceToOffice);
                Assert.True(WalkTime > NumWalkTimeToOffice);
                Assert.True(Distance > NumWalkDistanceToOffice);

                // Check the on foot route FROM the office 
                driver.FindElement(By.CssSelector(".PLEQOe.reverse")).Click();
                var TextWalkTimeFromOffice = driver.FindElement(By.CssSelector(".Fk3sm.fontHeadlineSmall")).Text;
                var TextWalkDistanceFromOffice = driver.FindElement(By.CssSelector(".ivN21e.tUEI8e.fontBodyMedium")).Text;

                var ResultWalkTimeFromOffice = new string(TextWalkTimeFromOffice.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultWalkDistanceFromOffice = new string(TextWalkDistanceFromOffice.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                Double.TryParse(ResultWalkTimeFromOffice, out double NumWalkTimeFromOffice);
                Double.TryParse(ResultWalkDistanceFromOffice, out double NumWalkDistanceFromOffice);
                Assert.True(WalkTime > NumWalkTimeFromOffice);
                Assert.True(Distance > NumWalkDistanceFromOffice);

                // Check the cycling route FROM the office
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[aria-label='Na rowerze']")));
                driver.FindElement(By.CssSelector("[aria-label='Na rowerze']")).Click();
                var TextBicycleTimeFromOffice = driver.FindElement(By.CssSelector(".Fk3sm.fontHeadlineSmall")).Text;
                var TextBicycleDistanceFromOffice = driver.FindElement(By.CssSelector(".ivN21e.tUEI8e.fontBodyMedium")).Text;

                var ResultBicycleTimeFromOffice = new string(TextBicycleTimeFromOffice.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultBicycleDistanceFromOffice = new string(TextBicycleDistanceFromOffice.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                Double.TryParse(ResultBicycleTimeFromOffice, out double NumBicycleTimeFromOffice);
                Double.TryParse(ResultBicycleDistanceFromOffice, out double NumBicycleDistanceFromOffice);
                Assert.True(BicycleTime > NumBicycleTimeFromOffice);
                Assert.True(Distance > NumBicycleDistanceFromOffice);

                // Check the cycling route TO the office
                driver.FindElement(By.CssSelector(".PLEQOe.reverse")).Click();
                var TextBicycleTimeToOffice = driver.FindElement(By.CssSelector(".Fk3sm.fontHeadlineSmall")).Text;
                var TextBicycleDistanceToOffice = driver.FindElement(By.CssSelector(".ivN21e.tUEI8e.fontBodyMedium")).Text;

                var ResultBicycleTimeToOffice = new string(TextBicycleTimeFromOffice.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultBicycleDistanceToOffice = new string(TextBicycleDistanceFromOffice.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                Double.TryParse(ResultBicycleTimeToOffice, out double NumBicycleTimeToOffice);
                Double.TryParse(ResultBicycleDistanceToOffice, out double NumBicycleDistanceToOffice);
                Assert.True(BicycleTime > NumBicycleTimeToOffice);
                Assert.True(Distance > NumBicycleDistanceToOffice);
            }          
                       
        }

    }
}

