using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace AxaTests1
{
    internal static class WebDriverInfra
    {
        public static IWebDriver Create_Browser(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return new ChromeDriver();
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                default:
                    throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
            }
        }

    }
}