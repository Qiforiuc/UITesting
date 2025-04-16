using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

namespace MobileUITesting.POMs;

public class PaymentByBiometrics
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By pageLabel;
    private readonly By yesButton;
    private readonly By laterButton;
    
    public PaymentByBiometrics(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                break;
            case MobilePlatform.iOS:
                pageLabel = MobileBy.XPath("//XCUIElementTypeStaticText[@name=\"Payment by biometrics\"]");
                yesButton = MobileBy.AccessibilityId("Yes");
                laterButton = MobileBy.AccessibilityId("Later");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
}