using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class LogoutPopUp
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By yesButton;

    public LogoutPopUp(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                yesButton = MobileBy.Id("android:id/button1");
                break;
            case MobilePlatform.iOS:
                yesButton = MobileBy.AccessibilityId("Yes");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public void pressOnYesButton()
    {
        var _yesButton = _wait.Until(ExpectedConditions.ElementIsVisible(yesButton));
        Thread.Sleep(100);
        _yesButton.Click();
    }

}