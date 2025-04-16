using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class IdnpPage
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;
    
    private readonly By pageLabel;
    private readonly By idnpInputField;
    private readonly By nextButton;

    public IdnpPage(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                pageLabel = MobileBy.XPath("//android.widget.TextView[@resource-id=\"md.maib.maibank.debug:id/screen_title_view\"]");
                idnpInputField =
                    MobileBy.XPath("//android.widget.EditText[@resource-id=\"md.maib.maibank.debug:id/idnp_field\"]");
                nextButton = MobileBy.XPath("//android.widget.Button[@resource-id=\"md.maib.maibank.debug:id/continue_button\"]");
                
                break;
            case MobilePlatform.iOS:
                pageLabel = MobileBy.AccessibilityId("auth.info-label");
                idnpInputField = MobileBy.XPath("//XCUIElementTypeTextField[@value=\"0000000000000\"]");
                nextButton = MobileBy.AccessibilityId("auth.continue-button");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public void inputIdnp(string idnp)
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(idnpInputField)).SendKeys(idnp);
    }

    public bool isOnCorrectScreen()
    {
        return _wait.Until(ExpectedConditions.ElementIsVisible(pageLabel)).Displayed;
    }
    
    public void pressOnNextButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(nextButton)).Click();
    }
}