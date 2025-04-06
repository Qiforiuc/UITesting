using MobileUITesting.Configurations;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System.Collections.Concurrent;
using Allure.NUnit;

namespace MobileUITesting.Tests;

public class BaseTest
{
    private readonly ThreadLocal<AndroidDriver> _driver = new();
    protected AppSettings Config;
    protected WebDriverWait Wait;

    protected string EmulatorName;
    protected string AppiumServerUrl;

    public AndroidDriver Driver => _driver.Value;

    public static readonly string[] Devices = { "emulator-5554", "emulator-5556" };

    [SetUp]
    public void Setup()
    {
        Config = ConfigManager.LoadSettings();
        AppiumServerUrl = ResolveAppiumUrl(EmulatorName, Config.AppiumServerUrls);

        var options = CreateAppiumOptions();

        _driver.Value = new AndroidDriver(new Uri(AppiumServerUrl), options);
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            Driver?.Quit();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while quitting driver: {e.Message}");
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

    private AppiumOptions CreateAppiumOptions()
    {
        var options = new AppiumOptions();
        options.PlatformName = Config.PlatformName;
        options.DeviceName = EmulatorName;
        options.AutomationName = Config.AutomationName;
        options.App = Config.AppPath;

        options.AddAdditionalAppiumOption("autoGrantPermissions", true);
        options.AddAdditionalAppiumOption("autoAcceptAlerts", true);
        options.AddAdditionalAppiumOption("appium:udid", EmulatorName);
        options.AddAdditionalAppiumOption("args", new[] { "arg1=value1", "arg2=value2" });

        return options;
    }

    private string ResolveAppiumUrl(string emulatorName, string[] urls)
    {
        return emulatorName switch
        {
            "emulator-5554" => urls.ElementAtOrDefault(0),
            "emulator-5556" => urls.ElementAtOrDefault(1),
            _ => urls.ElementAtOrDefault(2) ?? throw new ArgumentException("No Appium URL found for emulator.")
        };
    }
}
