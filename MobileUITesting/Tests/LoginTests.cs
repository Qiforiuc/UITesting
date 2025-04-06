using Allure.NUnit; // Correct namespace
using NUnit.Allure.Attributes;  // For attributes like [AllureSeverity]
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeverityLevel = Allure.Net.Commons.SeverityLevel;

namespace MobileUITesting.Tests;
[TestFixture]
[Parallelizable(ParallelScope.All)]
[AllureNUnit]
public class LoginTests:BaseTest
{
    public LoginTests()
    {
        EmulatorName = Devices[0];
    }
    
    [Test]
    [AllureTag("Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureOwner("Adrian")]
    public void LoginWithValidCredentials()
    {
        var element =
            Wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//android.widget.TextView[@text=\"Policy management\"]")));
        
        Assert.That(element, Is.Not.Null);
    }
}