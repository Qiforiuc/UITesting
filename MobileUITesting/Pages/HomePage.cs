using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class HomePage
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;
    
    private readonly By pageLabel;
    private readonly By userLabel;

    
    public HomePage(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                pageLabel = MobileBy.Id("md.maib.maibank.debug:id/header_bg_view");
                userLabel = MobileBy.Id("md.maib.maibank.debug:id/profile_photo_view");
                break;
            case MobilePlatform.iOS:
                pageLabel = MobileBy.AccessibilityId("HomeFeed/basic-background");
                userLabel = MobileBy.AccessibilityId("user pic placeholder");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public bool isOnCorrectScreen()
    {
        return _wait.Until(ExpectedConditions.ElementIsVisible(userLabel)).Displayed;
    }
}