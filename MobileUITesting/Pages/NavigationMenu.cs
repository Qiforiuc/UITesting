using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class NavigationMenu
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;
    
    private readonly By homeButton;
    private readonly By transfersButton;
    private readonly By lifeButton;
    private readonly By productsButton;
    private readonly By moreButton;
    
    public NavigationMenu(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                homeButton = MobileBy.AccessibilityId("Home");
                transfersButton = MobileBy.AccessibilityId("Payments");
                lifeButton = MobileBy.AccessibilityId("Life");
                productsButton = MobileBy.AccessibilityId("Products");
                moreButton = MobileBy.AccessibilityId("More");
                break;
            case MobilePlatform.iOS:
                homeButton = MobileBy.AccessibilityId("Home");
                transfersButton = MobileBy.AccessibilityId("Transfers");
                lifeButton = MobileBy.AccessibilityId("Life");
                productsButton = MobileBy.AccessibilityId("Products");
                moreButton = MobileBy.AccessibilityId("More");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public void pressOnHomeButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(homeButton)).Click();
    }
    
    public void pressOnTransfersButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(transfersButton)).Click();
    }
    
    public void pressOnLifeButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(lifeButton)).Click();
    }
    
    public void pressOnProductsButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(productsButton)).Click();
    }
    
    public void pressOnMoreButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(moreButton)).Click();
    }
}