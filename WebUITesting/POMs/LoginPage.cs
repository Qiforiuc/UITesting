using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;


namespace UITestFramework.POMs;

public class LoginPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    public readonly string Path = "/practice-test-login/";

    public LoginPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    private By _usernameField => By.Id("username");
    private By _passwordField => By.Id("password");
    private By _loginButton => By.Id("submit");
    
    public void EnterUsername(string username)
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(_usernameField)).SendKeys(username);
    }

    public void EnterPassword(string password)
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(_passwordField)).SendKeys(password);
    }

    public void ClickLoginButton()
    {
        _wait.Until(ExpectedConditions.ElementToBeClickable(_loginButton)).Click();
    }
}