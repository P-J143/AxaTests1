using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using System.Linq;


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
        public void ChromeWalkToOffice()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                driver.FindElement(By.Id("searchboxinput")).SendKeys("Chłodna 51 Warszawa");              
                driver.FindElement(By.ClassName("hArJGc")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input"))); // Wait for the page to load
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input")).SendKeys("Plac Defilad 1 Warszawa");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[1]/div[4]/button/img"))); // Wait for the Walk icon to load 
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[1]/div[4]/button/img")).Click(); // Click the walk icon
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[4]"))); //Wait for the results to load

                // Read how many minutes it takes to travel, how many kilometers and convert those into int/double
                var TextWalkTime = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]")).Text;
                var TextWalkDistance = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[2]")).Text;

                var decimalSeparator = ','; 
                var ResultWalkTime = new string(TextWalkTime.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultWalkDistance = new string(TextWalkDistance.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                Double.TryParse(ResultWalkTime, out double NumWalkTime);
                Double.TryParse(ResultWalkDistance, out double NumWalkDistance);

                //Assert that it takes less than 40 minutes and less than 3km
                Assert.True(WalkTime > NumWalkTime);
                Assert.True(Distance > NumWalkDistance);

            }
        }
        [Fact]
        public void ChromeRideToOffice()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                driver.FindElement(By.Id("searchboxinput")).SendKeys("Chłodna 51 Warszawa");
                driver.FindElement(By.ClassName("hArJGc")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input"))); // Wait for the page to load
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input")).SendKeys("Plac Defilad 1 Warszawa");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[2]/div[1]/div[1]/button/img"))); // Wait for the bicycle icon to load 
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[2]/div[1]/div[1]/button/img")).Click();// Click the bicycle icon
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]"))); //Wait for the results to load

                // Read how many minutes it takes to travel, how many kilometers and convert those into int/double
                var TextBicycleTime = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]")).Text;
                var TextBicycleDistance = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[2]")).Text;
                var decimalSeparator = ',';
                var ResultBicycleTime = new string(TextBicycleTime.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultBicycleDistance = new string(TextBicycleDistance.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());

                int NumBicycleTime = Convert.ToInt32(ResultBicycleTime);
                int NumBicycleDistance = Convert.ToInt32(ResultBicycleDistance);

                //Assert that it takes less than 15 minutes and less than 3km
                Assert.True(BicycleTime > NumBicycleTime);
                Assert.True(Distance > NumBicycleDistance);


            }
        }

        [Fact]
        public void ChromeWalkFromOffice()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                driver.FindElement(By.Id("searchboxinput")).SendKeys("Plac Defilad 1 Warszawa");
                driver.FindElement(By.ClassName("hArJGc")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input"))); // Wait for the page to load
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input")).SendKeys("Chłodna 51 Warszawa");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[1]/div[4]/button/img"))); // Wait for the Walk icon to load 
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[1]/div[4]/button/img")).Click(); // Click the walk icon
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[4]"))); //Wait for the results to load

                // Read how many minutes it takes to travel, how many kilometers and convert those into int/double
                var TextWalkTime = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]")).Text;
                var TextWalkDistance = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[2]")).Text;

                var decimalSeparator = ',';
                var ResultWalkTime = new string(TextWalkTime.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultWalkDistance = new string(TextWalkDistance.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                Double.TryParse(ResultWalkTime, out double NumWalkTime);
                Double.TryParse(ResultWalkDistance, out double NumWalkDistance);

                //Assert that it takes less than 40 minutes and less than 3km
                Assert.True(WalkTime > NumWalkTime);
                Assert.True(Distance > NumWalkDistance);

            }
        }
        [Fact]
        public void ChromeRideFromOffice()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                driver.FindElement(By.Id("searchboxinput")).SendKeys("Plac Defilad 1 Warszawa");
                driver.FindElement(By.ClassName("hArJGc")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input"))); // Wait for the page to load
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input")).SendKeys("Chłodna 51 Warszawa");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[2]/div[1]/div[1]/button/img"))); // Wait for the bicycle icon to load 
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[2]/div[1]/div[1]/button/img")).Click();// Click the bicycle icon
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]"))); //Wait for the results to load

                // Read how many minutes it takes to travel, how many kilometers and convert those into int/double
                var TextBicycleTime = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]")).Text;
                var TextBicycleDistance = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[2]")).Text;
                var decimalSeparator = ',';
                var ResultBicycleTime = new string(TextBicycleTime.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultBicycleDistance = new string(TextBicycleDistance.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());

                int NumBicycleTime = Convert.ToInt32(ResultBicycleTime);
                int NumBicycleDistance = Convert.ToInt32(ResultBicycleDistance);

                //Assert that it takes less than 15 minutes and less than 3km
                Assert.True(BicycleTime > NumBicycleTime);
                Assert.True(Distance > NumBicycleDistance);


            }
        }
        [Fact]
        public void FirefoxWalkToOffice()
        {
            using (IWebDriver driver = new FirefoxDriver())
            {

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                driver.FindElement(By.Id("searchboxinput")).SendKeys("Chłodna 51 Warszawa");
                driver.FindElement(By.ClassName("hArJGc")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input"))); // Wait for the page to load
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input")).SendKeys("Plac Defilad 1 Warszawa");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[1]/div[4]/button/img"))); // Wait for the Walk icon to load 
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[1]/div[4]/button/img")).Click(); // Click the walk icon
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[4]"))); //Wait for the results to load

                // Read how many minutes it takes to travel, how many kilometers and convert those into int/double
                var TextWalkTime = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]")).Text;
                var TextWalkDistance = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[2]")).Text;

                var decimalSeparator = ',';
                var ResultWalkTime = new string(TextWalkTime.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultWalkDistance = new string(TextWalkDistance.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                Double.TryParse(ResultWalkTime, out double NumWalkTime);
                Double.TryParse(ResultWalkDistance, out double NumWalkDistance);

                //Assert that it takes less than 40 minutes and less than 3km
                Assert.True(WalkTime > NumWalkTime);
                Assert.True(Distance > NumWalkDistance);

            }
        }
        [Fact]
        public void FirefoxRideToOffice()
        {
            using (IWebDriver driver = new FirefoxDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                driver.FindElement(By.Id("searchboxinput")).SendKeys("Chłodna 51 Warszawa");
                driver.FindElement(By.ClassName("hArJGc")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input"))); // Wait for the page to load
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input")).SendKeys("Plac Defilad 1 Warszawa");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[2]/div[1]/div[1]/button/img"))); // Wait for the bicycle icon to load 
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[2]/div[1]/div[1]/button/img")).Click();// Click the bicycle icon
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]"))); //Wait for the results to load

                // Read how many minutes it takes to travel, how many kilometers and convert those into int/double
                var TextBicycleTime = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]")).Text;
                var TextBicycleDistance = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[2]")).Text;
                var decimalSeparator = ',';
                var ResultBicycleTime = new string(TextBicycleTime.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultBicycleDistance = new string(TextBicycleDistance.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());

                int NumBicycleTime = Convert.ToInt32(ResultBicycleTime);
                int NumBicycleDistance = Convert.ToInt32(ResultBicycleDistance);

                //Assert that it takes less than 15 minutes and less than 3km
                Assert.True(BicycleTime > NumBicycleTime);
                Assert.True(Distance > NumBicycleDistance);


            }
        }

        [Fact]

        public void FirefoxWalkFromOffice()
        {
            using (IWebDriver driver = new FirefoxDriver())
            {

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                driver.FindElement(By.Id("searchboxinput")).SendKeys("Plac Defilad 1 Warszawa");
                driver.FindElement(By.ClassName("hArJGc")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input"))); // Wait for the page to load
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input")).SendKeys("Chłodna 51 Warszawa");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[1]/div[4]/button/img"))); // Wait for the Walk icon to load 
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[1]/div[4]/button/img")).Click(); // Click the walk icon
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[4]"))); //Wait for the results to load

                // Read how many minutes it takes to travel, how many kilometers and convert those into int/double
                var TextWalkTime = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]")).Text;
                var TextWalkDistance = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[2]")).Text;

                var decimalSeparator = ',';
                var ResultWalkTime = new string(TextWalkTime.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultWalkDistance = new string(TextWalkDistance.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                Double.TryParse(ResultWalkTime, out double NumWalkTime);
                Double.TryParse(ResultWalkDistance, out double NumWalkDistance);

                //Assert that it takes less than 40 minutes and less than 3km
                Assert.True(WalkTime > NumWalkTime);
                Assert.True(Distance > NumWalkDistance);

            }
        }
        [Fact]
        public void FirefoxRideFromOffice()
        {
            using (IWebDriver driver = new FirefoxDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(HomeUrl);
                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                driver.FindElement(By.Id("searchboxinput")).SendKeys("Plac Defilad 1 Warszawa");
                driver.FindElement(By.ClassName("hArJGc")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input"))); // Wait for the page to load
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[3]/div[1]/div[1]/div[2]/div[1]/div/input")).SendKeys("Chłodna 51 Warszawa");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[2]/div[1]/div[1]/button/img"))); // Wait for the bicycle icon to load 
                driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[3]/div[1]/div[2]/div/div[2]/div/div/div[2]/div[1]/div[1]/button/img")).Click();// Click the bicycle icon
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]"))); //Wait for the results to load

                // Read how many minutes it takes to travel, how many kilometers and convert those into int/double
                var TextBicycleTime = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[1]")).Text;
                var TextBicycleDistance = driver.FindElement(By.XPath("/html/body/div[3]/div[9]/div[8]/div/div[1]/div/div/div[4]/div[1]/div[1]/div[3]/div[1]/div[2]")).Text;
                var decimalSeparator = ',';
                var ResultBicycleTime = new string(TextBicycleTime.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());
                var ResultBicycleDistance = new string(TextBicycleDistance.Where(c => char.IsDigit(c) || c == decimalSeparator).ToArray());

                int NumBicycleTime = Convert.ToInt32(ResultBicycleTime);
                int NumBicycleDistance = Convert.ToInt32(ResultBicycleDistance);

                //Assert that it takes less than 15 minutes and less than 3km
                Assert.True(BicycleTime > NumBicycleTime);
                Assert.True(Distance > NumBicycleDistance);


            }
        }
    }
}

