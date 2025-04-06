using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Assert = NUnit.Framework.Assert;

namespace MobileUITesting.Tests;
[TestFixture]
[Parallelizable(ParallelScope.All)]
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
        var element = Wait
            .Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//android.widget.TextView[@resource-id=\"android:id/title\" and @text=\"Accessibility\"]")));
        
        Assert.That(element, Is.Not.Null);
    }
}