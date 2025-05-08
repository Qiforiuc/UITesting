using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UITestFramework.Pages;

public class PracticePage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    public readonly string Path = "/practice-test-exceptions/";
    
    public PracticePage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }
    
    private By _addButton = By.Id("add_btn");
    private By _confirmationMessage = By.Id("confirmation");
    
    public void ClickAddButton()
    {
        _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_addButton)).Click();
    }
    
    public string GetConfirmationMessage()
    {
        return _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(_confirmationMessage)).Text;
    }
    
}