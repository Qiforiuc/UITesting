using Allure.Commons;
using MobileUITesting.Configurations;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using MobileUITesting.DriverFactory;
using MobileUITesting.POMs;
using MobileUITesting.TestDataFactory;

namespace MobileUITesting.Tests;
public class BaseTest
{
    private readonly ThreadLocal<AppiumDriver> _driver = new();
    
    protected WebDriverWait Wait;
    protected readonly DeviceConfig _deviceConfig;
    public AppiumDriver Driver => _driver.Value;

    public User User;
    public BaseTest(DeviceConfig deviceConfig)
    {
        _deviceConfig = deviceConfig;
    }

    [SetUp]
    public void Setup()
    {
        var driverFactory = CreateFactoryForPlatform(_deviceConfig.Platform);
        _driver.Value = driverFactory.CreateDriver(_deviceConfig, _deviceConfig.AutomationName);
        User = new User(_deviceConfig);
        
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
    }
    
    [TearDown]
    public void TearDown()
    {
        try
        {
            Driver?.Quit();
        }
        finally
        {
            _driver.Value = null;
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _driver.Dispose();
    }
    
    private IDriverFactory CreateFactoryForPlatform(MobilePlatform platform)
    {
        return platform switch
        {
            MobilePlatform.Android => new AndroidDriverFactory(),
            MobilePlatform.iOS => new IOSDriverFactory(),
            _ => throw new NotSupportedException($"Platform '{platform}' is not supported.")
        };
    }
    
    public void SetTestEnvironmentBackendURLs(LoginPage _loginPage)
    {
        _loginPage.pressOnBurgerMenuButton();
        _loginPage.pressOnDebugMenuButton();
        _loginPage.pressOnBackendUrlButton();
        _loginPage.pressOnBackendUrl();
        
        if(_deviceConfig.Platform.Equals(MobilePlatform.Android))
        {
            _loginPage.pressRestartButton();
            _loginPage.pressOnBurgerMenuButton();
            _loginPage.pressOnDebugMenuButton();
        }
        
        _loginPage.pressOnExternalBackendUrlButton();
        _loginPage.pressOnExternalUrl();
        if (_deviceConfig.Platform.Equals( MobilePlatform.Android))
        {
            _loginPage.pressRestartButton();
        }
    
        if (_deviceConfig.Platform.Equals(MobilePlatform.iOS))
        {
            _loginPage.pressBackButton();
        }
        
        _loginPage.checkMaibLogoIsVisible();
    }
}

