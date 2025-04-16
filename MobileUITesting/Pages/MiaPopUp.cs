using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class MiaPopUp
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By pageLabel;
    private readonly By enableButton;
    private readonly By closeButton;

    public MiaPopUp(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                pageLabel = MobileBy.XPath(
                    "//android.widget.TextView[@resource-id=\"md.maib.maibank.debug:id/title_view\"]");
                enableButton =
                    MobileBy.XPath("//android.widget.Button[@resource-id=\"md.maib.maibank.debug:id/accept_button\"]");
                closeButton = MobileBy.XPath("//android.widget.ImageButton[@content-desc=\"Navigate up\"]");
                break;
            case MobilePlatform.iOS:
                pageLabel = MobileBy.AccessibilityId("Mia");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public bool isOnCorrectScreen()
    {
        return _wait.Until(ExpectedConditions.ElementIsVisible(pageLabel)).Displayed;
    }
    
    public void pressOnEnableButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(enableButton)).Click();
    }
    
    public void pressOnCloseButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(closeButton)).Click();
    }
}