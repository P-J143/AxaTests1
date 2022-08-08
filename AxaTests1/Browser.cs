//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Firefox;
//using System.Collections.Generic;
//using System.Reflection;
//using Xunit.Sdk;

//namespace AxaTests1
//{
//    class Browser
//    {
//        public class BrowserAttribute : DataAttribute
//        {
//            private IWebDriver Driver { get; set; }
//            public BrowserAttribute(string browser, string url)
//            {
//                switch (browser)
//                {
//                    case "Chrome":
//                        Driver = new ChromeDriver();
//                        break;
//                    case "Firefox":
//                        Driver = new FirefoxDriver();
//                        break;
//                }
//                Driver.Manage().Window.Maximize();
//                Goto(url);
//            }

//            public void Goto(string url)
//            {
//                Driver.Url = url;
//            }

//            public override IEnumerable<object[]> GetData(MethodInfo testMethod)
//            {
//                return new[] { new object[] { Driver } };
//            }
//        }
//    }
//}
