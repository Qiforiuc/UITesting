using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

namespace MobileUITesting.POMs;

public class TermsAndConditionsPage
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By pageLabel;
    private readonly By acceptButton;
    
    public TermsAndConditionsPage(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                pageLabel = MobileBy.XPath("//android.widget.TextView[@text=\"Terms and conditions\"]");
                acceptButton = MobileBy.XPath("//android.widget.Button");
                break;
            case MobilePlatform.iOS:
                pageLabel = MobileBy.AccessibilityId("Terms and conditions");
                acceptButton = MobileBy.AccessibilityId("Accept");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public bool isOnCorrectScreen()
    {
        return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(pageLabel)).Displayed;
    }
    
    public void pressOnAcceptButton()
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(acceptButton)).Click();
    }
}