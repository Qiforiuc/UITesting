using Helpers;
using MobileUITesting.Configurations;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace MobileUITesting.DriverFactory;

public class IOSDriverFactory:IDriverFactory
{
    public AppiumDriver CreateDriver(DeviceConfig config, string automationName)
    {
        var options = new AppiumOptions();
        options.PlatformName = config.Platform.ToString();
        options.DeviceName = config.DeviceName;
        options.AutomationName = automationName;
        options.PlatformVersion = "17.0";
        options.App = config.AppPath;

        options.AddAdditionalAppiumOption("autoAcceptAlerts", true);
        options.AddAdditionalAppiumOption("udid", "B696AE98-4529-49DA-BA81-6C1CCBB5C553");
        
        options.AddAdditionalAppiumOption("noReset", true);
        options.AddAdditionalAppiumOption("fullReset", false);
        options.AddAdditionalAppiumOption("wdaLocalPort", 8100);
        
        IOSPermissionsHelper.GrantAllPermissions(config.AppPath);
        
        return new IOSDriver(new Uri(config.AppiumServerUrl), options);
    }
}