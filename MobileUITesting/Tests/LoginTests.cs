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
public class LoginTests:BaseTest
{
    private LoginPage _loginPage;
    private readonly DeviceConfig _deviceConfig;

    public LoginTests(MobilePlatform platform, string deviceName)
        : base(GetDeviceConfig(platform, deviceName))
    {
        _deviceConfig = GetDeviceConfig(platform, deviceName);
    }

    [SetUp]
    public void SetUpTest()
    {
        base.Setup();
        _loginPage = new LoginPage(Driver, Wait, _deviceConfig);
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
    public void SetTestEnvironmentBackendURLs()
    {
        if(_loginPage.checkAcceptButtonIsVisible())
        {
            _loginPage.pressAcceptButton();
        }

        _loginPage.pressOnBurgerMenuButton();
        _loginPage.pressOnDebugMenuButton();
        _loginPage.pressOnBackendUrlButton();
        _loginPage.pressOnBackendUrl();
        
        if(_deviceConfig.Platform == MobilePlatform.Android)
        {
            _loginPage.pressRestartButton();
            _loginPage.pressOnBurgerMenuButton();
            _loginPage.pressOnDebugMenuButton();
        }
        
        _loginPage.pressOnExternalBackendUrlButton();
        _loginPage.pressOnExternalUrl();
        if (_deviceConfig.Platform == MobilePlatform.Android)
        {
            _loginPage.pressRestartButton();
        }
    
        if (_deviceConfig.Platform == MobilePlatform.iOS)
        {
            _loginPage.pressBackButton();
        }
        
        _loginPage.checkMaibLogoIsVisible();
    }
}