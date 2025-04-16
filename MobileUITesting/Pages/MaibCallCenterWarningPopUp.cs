using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

namespace MobileUITesting.POMs;

public class MaibCallCenterWarningPopUp
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By pageLabel;
    private readonly By yesButton;
    
    public MaibCallCenterWarningPopUp(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                pageLabel = MobileBy.XPath("//android.widget.TextView[@text=\"Warning\"]");
                yesButton = MobileBy.XPath("//android.widget.Button[@resource-id=\"android:id/button1\"]");
                break;
            case MobilePlatform.iOS:
                pageLabel = MobileBy.AccessibilityId("Warning");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public bool isOnCorrectScreen()
    {
        return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(pageLabel)).Displayed;
    }
    
    public void pressOnYesButton()
    {
        var _yesButton = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(yesButton));
        Thread.Sleep(100);
        _yesButton.Click();
    }
}