using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class MorePage
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By logoutButton;

    public MorePage(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                logoutButton =
                    MobileBy.XPath(
                        "//android.widget.TextView[@resource-id=\"md.maib.maibank.debug:id/textTv\" and @text=\"Log out\"]");
                break;
            case MobilePlatform.iOS:
                logoutButton = MobileBy.AccessibilityId("Logout");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public void pressOnLogoutButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(logoutButton)).Click();
    }
}