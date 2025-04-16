using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class Pin5Page
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By pageLabel;
    private readonly By pinButton1;
    private readonly By pinButton2;
    private readonly By pinButton3;
    private readonly By pinButton4;
    private readonly By pinButton5;
    private readonly By pinButton6;
    private readonly By pinButton7;
    private readonly By pinButton8;
    private readonly By pinButton9;
    private readonly By pinButton0;
    private readonly By forgotPinButton;
    
    public Pin5Page(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                pageLabel = MobileBy.XPath("//android.widget.TextView[@resource-id=\"md.maib.maibank.debug:id/screen_title_view\"]");
                pinButton1 = MobileBy.XPath("//android.widget.TextView[@text=\"1\"]");
                pinButton2 = MobileBy.XPath("//android.widget.TextView[@text=\"2\"]");
                pinButton3 = MobileBy.XPath("//android.widget.TextView[@text=\"3\"]");
                pinButton4 = MobileBy.XPath("//android.widget.TextView[@text=\"4\"]");
                pinButton5 = MobileBy.XPath("//android.widget.TextView[@text=\"5\"]");
                pinButton6 = MobileBy.XPath("//android.widget.TextView[@text=\"6\"]");
                pinButton7 = MobileBy.XPath("//android.widget.TextView[@text=\"7\"]");
                pinButton8 = MobileBy.XPath("//android.widget.TextView[@text=\"8\"]");
                pinButton9 = MobileBy.XPath("//android.widget.TextView[@text=\"9\"]");
                pinButton0 = MobileBy.XPath("//android.widget.TextView[@text=\"0\"]");
                break;
            case MobilePlatform.iOS:
                pageLabel = MobileBy.XPath("//XCUIElementTypeStaticText[@name=\"Enter access password\"]");
                pinButton1 = MobileBy.XPath("//XCUIElementTypeButton[@name=\"1\"]");
                pinButton2 = MobileBy.XPath("//XCUIElementTypeButton[@name=\"2\"]");
                pinButton3 = MobileBy.XPath("//XCUIElementTypeButton[@name=\"3\"]");
                pinButton4 = MobileBy.XPath("//XCUIElementTypeButton[@name=\"4\"]");
                pinButton5 = MobileBy.XPath("//XCUIElementTypeButton[@name=\"5\"]");
                pinButton6 = MobileBy.XPath("//XCUIElementTypeButton[@name=\"6\"]");
                pinButton7 = MobileBy.XPath("//XCUIElementTypeButton[@name=\"7\"]");
                pinButton8 = MobileBy.XPath("//XCUIElementTypeButton[@name=\"8\"]");
                pinButton9 = MobileBy.XPath("//XCUIElementTypeButton[@name=\"9\"]");
                pinButton0 = MobileBy.XPath("//XCUIElementTypeButton[@name=\"0\"]");
                forgotPinButton = MobileBy.XPath("//XCUIElementTypeStaticText[@name=\"I don't have the password\"]");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public bool isOnCorrectScreen()
    {
        return _wait.Until(ExpectedConditions.ElementIsVisible(pageLabel)).Displayed;
    }
    
    public void enterPin(string pin)
    {
        foreach (char digit in pin)
        {
            switch (digit)
            {
                case '0':
                    _wait.Until(ExpectedConditions.ElementIsVisible(pinButton0)).Click();
                    break;
                case '1':
                    _wait.Until(ExpectedConditions.ElementIsVisible(pinButton1)).Click();
                    break;
                case '2':
                    _wait.Until(ExpectedConditions.ElementIsVisible(pinButton2)).Click();
                    break;
                case '3':
                    _wait.Until(ExpectedConditions.ElementIsVisible(pinButton3)).Click();
                    break;
                case '4':
                    _wait.Until(ExpectedConditions.ElementIsVisible(pinButton4)).Click();
                    break;
                case '5':
                    _wait.Until(ExpectedConditions.ElementIsVisible(pinButton5)).Click();
                    break;
                case '6':
                    _wait.Until(ExpectedConditions.ElementIsVisible(pinButton6)).Click();
                    break;
                case '7':
                    _wait.Until(ExpectedConditions.ElementIsVisible(pinButton7)).Click();
                    break;
                case '8':
                    _wait.Until(ExpectedConditions.ElementIsVisible(pinButton8)).Click();
                    break;
                case '9':
                    _wait.Until(ExpectedConditions.ElementIsVisible(pinButton9)).Click();
                    break;
            }
        }
    }
}