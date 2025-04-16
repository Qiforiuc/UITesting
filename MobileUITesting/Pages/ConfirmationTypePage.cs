using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class ConfirmationTypePage
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;
    
    private readonly By pageLabel;
    private readonly By continueButton;
    private readonly By loginWithoutBiometricsButton;

    
    public ConfirmationTypePage(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                pageLabel = MobileBy.XPath("//android.widget.TextView[@resource-id=\"md.maib.maibank.debug:id/screen_title_view\"]");
                continueButton = MobileBy.XPath("//android.widget.Button[@resource-id=\"md.maib.maibank.debug:id/accept_button\"]");
                loginWithoutBiometricsButton = MobileBy.XPath("//android.widget.Button[@text=\"Login without biometrics\"]");
                break;
            case MobilePlatform.iOS:
                pageLabel = MobileBy.AccessibilityId("Get ready for a selfie");
                continueButton = MobileBy.XPath("//XCUIElementTypeButton[@name=\"Continue\"]");
                loginWithoutBiometricsButton = MobileBy.XPath("//XCUIElementTypeStaticText[@name=\"Login without biometrics\"]");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public bool isOnCorrectScreen()
    {
        return _wait.Until(ExpectedConditions.ElementIsVisible(pageLabel)).Displayed;
    }
    
    public void pressOnContinueButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(continueButton)).Click();
    }
    
    public void pressOnLoginWithoutBiometricsButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(loginWithoutBiometricsButton)).Click();
    }
}