using MobileUITesting.Configurations;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace MobileUITesting.DriverFactory;

public interface IDriverFactory
{
    AppiumDriver CreateDriver(DeviceConfig deviceConfig, string automationName);
}