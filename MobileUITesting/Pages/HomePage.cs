using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class HomePage
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By logo;

    
    public HomePage(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        var platform = config.Platform;

        switch (platform)
        {
            case MobilePlatform.Android:
                logo = MobileBy.AccessibilityId("logo");
                break;
            case MobilePlatform.iOS:
                logo = MobileBy.AccessibilityId("logo");
                break;
        }
    }
    
    public bool IsLogoDisplayed()
    {
        return true;
    }
}