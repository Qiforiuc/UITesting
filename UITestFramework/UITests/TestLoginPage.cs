using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using UITestFramework.Configurations;
using UITestFramework.Pages;

namespace UITestFramework.UITests;

public class TestLoginPage : BaseTest
{
    [Test]
    public void testSuccessfulLoginOnThePage()
    {
        var LoginPage = new LoginPage(Driver, Wait);
        
        Driver.Navigate().GoToUrl(Config.BaseUrl + "practice-test-login/");
        
        LoginPage.EnterUsername(Config.Credentials.Username);
        LoginPage.EnterPassword(Config.Credentials.Password);
        LoginPage.ClickLoginButton();
        
        Wait.Until(d => d.Url.Contains("/logged-in-successfully"));
        
        IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
        js.ExecuteScript("window.scrollBy(0, 500);");
        
        Assert.That(Driver.Url.Contains("/logged-in-successfully"));
        
        
    }
}