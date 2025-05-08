using Allure.NUnit;
using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using MobileUITesting.POMs; // Correct namespace
using NUnit.Allure.Attributes;  // For attributes like [AllureSeverity]
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using SeverityLevel = Allure.Net.Commons.SeverityLevel;

namespace MobileUITesting.Tests;
[TestFixture(MobilePlatform.Android, "emulator-5554")]
[TestFixture(MobilePlatform.iOS, "iPhone 15 Pro")]
[Parallelizable(ParallelScope.All)]
[AllureNUnit]
public class HomePageTests:BaseTest
{
    private HomePage _homePage;
    private readonly DeviceConfig _deviceConfig;

    public HomePageTests(MobilePlatform platform, string deviceName)
        : base(GetDeviceConfig(platform, deviceName))
    {
        _deviceConfig = GetDeviceConfig(platform, deviceName);
    }

    [SetUp]
    public void SetUpTest()
    {
        base.Setup();
        _homePage = new HomePage(Driver, Wait, _deviceConfig);
    }
    
    private static DeviceConfig GetDeviceConfig(MobilePlatform platform, string deviceName)
    {
        var config = ConfigManager.LoadSettings()
            .Devices
            .FirstOrDefault(d =>
                d.Platform == platform &&
                d.DeviceName.Equals(deviceName, StringComparison.OrdinalIgnoreCase));

        if (config == null)
            throw new ArgumentException($"No matching DeviceConfig found for platform '{platform}' and device name '{deviceName}'");

        return config;
    }
    
    public static IEnumerable<DeviceConfig> DeviceConfigs =>
        ConfigManager.LoadSettings().Devices;

    
    [Test(Description = $"[]Set Test Environment Backend URLs")]
    [AllureTag("Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureOwner("Adrian")]
    public void VerifyLogoIsDisplayed()
    {
        _homePage.IsLogoDisplayed();
        
        Assert.That(_homePage.IsLogoDisplayed().Equals(true), "Logo is not displayed");
    }
}