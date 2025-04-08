using System.Linq.Expressions;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using SeleniumExtras.WaitHelpers;
using Assert = NUnit.Framework.Assert;

namespace MobileUITesting.Tests;
[TestFixture]
[Parallelizable(ParallelScope.All)]
[AllureNUnit]
public class HomePageTests:BaseTest
{
    public HomePageTests()
    {
        EmulatorName = Devices[1];
    }
    
    [Test]
    [AllureTag("Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureOwner("Adrian")]
    public void LoginWithInvalidCredentials()
    {
        var accessibilityTitle = Wait
            .Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//android.widget.TextView[@resource-id=\"android:id/title\" and @text=\"Accessibility\"]")));
            
        var element = Driver.FindElement(MobileBy.AndroidUIAutomator(
            "new UiScrollable(new UiSelector().scrollable(true))" +
            ".scrollIntoView(new UiSelector().text(\"Request to manage credentials\"))"));
        
        element.Click();

        var titleElement = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("1")));
        titleElement.Click();
        
        var nameElement = Driver.FindElement(MobileBy.AccessibilityId("1"));
        
        var okButtonElement = Wait
            .Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//android.widget.Button[@resource-id=\"android:id/button1\"]")));
        
        okButtonElement.Click();
        
        Assert.That(accessibilityTitle, Is.Not.Null);
    }
}