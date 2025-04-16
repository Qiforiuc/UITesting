using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MobileUITesting.POMs;

public class LoginPage
{
    private readonly AppiumDriver _driver;
    private readonly WebDriverWait _wait;

    private readonly By burgerMenu;
    private readonly By debugMenu;
    private readonly By backendUrlButton;
    private readonly By backendUrl;
    private readonly By externalBackendUrlButton;
    private readonly By externalUrl;
    private readonly By maibLogo;
    private readonly By backButton;
    private readonly By acceptButton;
    private readonly By restartButton;
    private readonly By loginButton;
    
    public LoginPage(AppiumDriver driver, WebDriverWait wait, DeviceConfig config)
    {
        _driver = driver;
        _wait = wait;

        switch (config.Platform)
        {
            case MobilePlatform.Android:
                burgerMenu = MobileBy.XPath("//android.widget.ImageButton[@content-desc=\"Open\"]");
                debugMenu = MobileBy.XPath(
                    "//android.widget.ImageView[@resource-id=\"md.maib.maibank.debug:id/image_header\"]");
                backendUrlButton =
                    MobileBy.XPath(
                        "//androidx.compose.ui.platform.ComposeView/android.view.View/android.view.View/android.view.View[1]/android.view.View[8]/android.widget.Button");
                backendUrl =
                    MobileBy.XPath(
                        "//android.widget.RadioButton[@resource-id=\"md.maib.maibank.debug:id/end_point_radio\" and @text=\"https://testpay.maib.md:8443/MAIBankService\"]");
                externalBackendUrlButton =
                    MobileBy.XPath(
                        "//androidx.compose.ui.platform.ComposeView/android.view.View/android.view.View/android.view.View[1]/android.view.View[9]/android.widget.Button");
                externalUrl =
                    MobileBy.XPath(
                        "//android.widget.RadioButton[@resource-id=\"md.maib.maibank.debug:id/end_point_radio\" and @text=\"https://testpay.maib.md:8443\"]");
                maibLogo = MobileBy.XPath(
                    "//android.widget.ImageView[@resource-id=\"md.maib.maibank.debug:id/ic_logo\"]");
                backButton =
                    MobileBy.XPath(
                        "//androidx.compose.ui.platform.ComposeView/android.view.View/android.view.View/android.view.View[2]/android.widget.Button");
                acceptButton = MobileBy.XPath("//android.widget.Button");
                restartButton = MobileBy.XPath("//android.widget.Button[@resource-id=\"android:id/button1\"]");
                loginButton = MobileBy.Id("md.maib.maibank.debug:id/tv_login");
                break;
            case MobilePlatform.iOS:
                burgerMenu = MobileBy.AccessibilityId("menu icon");
                debugMenu = MobileBy.XPath("//XCUIElementTypeWindow/XCUIElementTypeOther/XCUIElementTypeOther/XCUIElementTypeOther/XCUIElementTypeOther/XCUIElementTypeOther/XCUIElementTypeOther/XCUIElementTypeOther[2]/XCUIElementTypeOther/XCUIElementTypeImage");
                backendUrlButton = MobileBy.AccessibilityId("Choose endpoint");
                backendUrl = MobileBy.AccessibilityId("https://testpay.maib.md:8443/MAIBankService");
                externalBackendUrlButton = MobileBy.AccessibilityId("Choose auth endpoint");
                externalUrl = MobileBy.AccessibilityId("https://testpay.maib.md:8443");
                maibLogo = MobileBy.XPath("//XCUIElementTypeScrollView/XCUIElementTypeOther[1]/XCUIElementTypeOther[1]/XCUIElementTypeImage");
                backButton = MobileBy.AccessibilityId("Back");
                acceptButton = MobileBy.XPath("//XCUIElementTypeButton[@name=\"Accept\"]");
                restartButton = MobileBy.XPath("//XCUIElementTypeButton[@name=\"Restart\"]");
                loginButton = MobileBy.XPath("//XCUIElementTypeButton[@name=\"Enter\"]");
                break;
            default:
                throw new NotSupportedException($"Platform '{config.Platform}' is not supported.");
        }
    }
    
    public void pressOnBurgerMenuButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(burgerMenu)).Click();
    }

    public void pressOnDebugMenuButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(debugMenu)).Click();
    }

    public void pressOnBackendUrlButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(backendUrlButton)).Click();
    }

    public void pressOnBackendUrl()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(backendUrl)).Click();
    }

    public void pressOnExternalBackendUrlButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(externalBackendUrlButton)).Click();
    }

    public void pressOnExternalUrl()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(externalUrl)).Click();
    }

    public void checkMaibLogoIsVisible()
    {
        var _maibLogo = _wait.Until(ExpectedConditions.ElementIsVisible(maibLogo));
        Assert.That(_maibLogo.Displayed, "Maib Logo is not visible!");
    }

    public void pressBackButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(backButton)).Click();
    }

    public void pressAcceptButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(acceptButton)).Click();
    }
    
    public void pressRestartButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(restartButton)).Click();
    }
    
    public bool checkAcceptButtonIsVisible()
    {
        try
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(acceptButton)).Displayed;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }
    
    public void pressLoginButton()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(loginButton)).Click();
    }

    public bool isOnCorrectScreen()
    {
        return _wait.Until(ExpectedConditions.ElementIsVisible(maibLogo)).Displayed;
    }
}