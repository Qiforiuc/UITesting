using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class PhoneNumberPage
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By pageLabel;
    private readonly By phoneNumberInputField;
    private readonly By continueButton;
    
    public PhoneNumberPage(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                pageLabel = MobileBy.XPath("//android.widget.TextView[@text=\"Enter your\nphone number\"]");
                phoneNumberInputField = MobileBy.XPath("//android.widget.EditText");
                continueButton = MobileBy.XPath("//androidx.compose.ui.platform.ComposeView/android.view.View/android.view.View/android.view.View[2]/android.widget.Button");
                break;
            case MobilePlatform.iOS:
                pageLabel = MobileBy.AccessibilityId("Enter phone number");
                phoneNumberInputField = MobileBy.XPath("//XCUIElementTypeTextField[@value=\"0\"]");
                continueButton = MobileBy.AccessibilityId("Continue");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public bool isOnCorrectScreen()
    {
        return _wait.Until(ExpectedConditions.ElementIsVisible(pageLabel)).Displayed;
    }
    
    public void inputPhoneNumber(string phoneNumber)
    {
        string trimmedPhone = phoneNumber.Substring(1);

        _wait.Until(ExpectedConditions.ElementIsVisible(phoneNumberInputField))
            .SendKeys(trimmedPhone);
    }

    
    public void pressOnContinueButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(continueButton)).Click();
    }
}