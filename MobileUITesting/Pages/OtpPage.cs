using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class OtpPage
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;
    private readonly DeviceConfig _config;

    private readonly By pageLabel;
    private readonly By otpInputField;
    private readonly By resendButton;
    
    public OtpPage(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;
        _config = config;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                pageLabel = MobileBy.XPath(
                    "//android.widget.TextView[@resource-id=\"md.maib.maibank.debug:id/screen_title_view\"]");
                otpInputField = MobileBy.XPath("//android.widget.EditText[@resource-id=\"md.maib.maibank.debug:id/code_field\"]");
                resendButton = MobileBy.XPath(
                    "//android.widget.TextView[@resource-id=\"md.maib.maibank.debug:id/resend_label\"]");
                break;
            case MobilePlatform.iOS:
                pageLabel = MobileBy.AccessibilityId("Enter the code received via SMS");
                otpInputField = MobileBy.XPath("//XCUIElementTypeTextField");
                resendButton = MobileBy.AccessibilityId("Resend");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public bool isOnCorrectScreen()
    {
        return _wait.Until(ExpectedConditions.ElementIsVisible(pageLabel)).Displayed;
    }
    
    public async Task inputOtp(string otp)
    {
        var inputElement = _wait.Until(ExpectedConditions.ElementIsVisible(otpInputField));

        if (_config.Platform.Equals(MobilePlatform.iOS))
        {
            foreach (char digit in otp)
            {
                inputElement.SendKeys(digit.ToString());
                await Task.Delay(200);
            }
        }
        else
        {
            inputElement.SendKeys(otp);
        }
    }
}