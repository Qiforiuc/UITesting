using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V133.Runtime;
using OpenQA.Selenium.Support.UI;
using UITestFramework.Configurations;

public class BaseTest
{
    protected IWebDriver Driver;
    protected AppConfig Config;
    protected WebDriverWait Wait;
    
    [SetUp]
    public void Initialize()
    {
        Config = ConfigReader.LoadConfig();
        
        if (Config.Browser.Equals("Chrome", StringComparison.OrdinalIgnoreCase))
        {
            var options = new ChromeOptions();
            if (Config.Headless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--window-size=1920,1080");
            }

            Driver = new ChromeDriver(options);
        }
        else
        {
            throw new Exception($"Unsupported browser: {Config.Browser}");
        }

        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.Timeouts.ImplicitWait);
        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Config.Timeouts.PageLoad);

        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Config.Timeouts.ExplicitWait));
    }

    [TearDown]
    public void Cleanup()
    {
       Driver?.Quit();
       Driver?.Dispose();
    }
}