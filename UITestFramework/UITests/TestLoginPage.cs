using Helpers;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using UITestFramework.Pages;

namespace UITestFramework.UITests;

public class TestLoginPage : BaseTest
{
    [Test]
    public void testSuccessfulLoginOnThePage()
    {
        var LoginPage = new LoginPage(Driver, Wait);
        
        Driver.Navigate().GoToUrl(Config.BaseUrl + LoginPage.Path);
        
        LoginPage.EnterUsername(Config.Credentials.Username);
        LoginPage.EnterPassword(Config.Credentials.Password);
        LoginPage.ClickLoginButton();
        
        Wait.Until(d => d.Url.Contains("/logged-in-successfully"));
        
        IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
        js.ExecuteScript("window.scrollBy(0, 500);");
        
        Assert.That(Driver.Url.Contains("/logged-in-successfully"), "Url must contain '/logged-in-successfully'");
        Assert.That(Driver.FindElement(By.XPath("//*[@id=\"loop-container\"]/div/article/div[1]/h1")).Displayed, "Header must be displayed");
        Assert.That(Driver.FindElement(By.XPath("//*[@id=\"loop-container\"]/div/article/div[1]/h1")).Text.Equals("Logged In Successfully", StringComparison.OrdinalIgnoreCase), "Header must be 'Logged In Successfully'");
        Assert.That(Driver.FindElement(By.XPath("//*[@id=\"loop-container\"]/div/article/div[2]/div/div/div/a")).Displayed, "Log out button must be displayed");
        Assert.That(Driver.FindElement(By.XPath("//*[@id=\"loop-container\"]/div/article/div[2]/div/div/div/a")).Text.Equals("Log out", StringComparison.OrdinalIgnoreCase), "Log out button must be 'Log out'");
        Assert.That(Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"loop-container\"]/div/article/div[2]/div/div/div/a"))).Enabled, "Log out button must be clickable");
    }
}