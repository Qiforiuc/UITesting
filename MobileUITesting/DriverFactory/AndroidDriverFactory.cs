using MobileUITesting.Configurations;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace MobileUITesting.DriverFactory;

public class AndroidDriverFactory:IDriverFactory
{
    public AppiumDriver CreateDriver(DeviceConfig config, string automationName)
    {
        var options = new AppiumOptions();
        options.PlatformName = "Android";
        options.DeviceName = config.DeviceName;
        options.AutomationName = automationName;
        options.App = config.AppPath;

        options.AddAdditionalAppiumOption("autoGrantPermissions", true);
        options.AddAdditionalAppiumOption("autoAcceptAlerts", true);
        options.AddAdditionalAppiumOption("appium:udid", config.DeviceName);
        options.AddAdditionalAppiumOption("noReset", false);
        options.AddAdditionalAppiumOption("fullReset", true);
        return new AndroidDriver(new Uri(config.AppiumServerUrl), options);
    }
}