using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using UITestFramework.Configurations;

public class BaseTest
{
    private static ThreadLocal<IWebDriver> driver = new();
    private static ThreadLocal<WebDriverWait> wait = new();
    protected AppConfig Config;

    protected IWebDriver Driver => driver.Value!;
    protected WebDriverWait Wait => wait.Value!;

    [SetUp]
    public void Initialize()
    {
        Config = ConfigReader.LoadConfig();

        if (Config.Browser.Equals("Chrome", StringComparison.OrdinalIgnoreCase))
        {
            var options = new ChromeOptions();

            if (Config.Headless)
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--window-size=1920,1080");
            }

            driver.Value = new ChromeDriver(options);
        }
        else
        {
            throw new Exception($"Unsupported browser: {Config.Browser}");
        }

        driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.Timeouts.ImplicitWait);
        driver.Value.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Config.Timeouts.PageLoad);

        wait.Value = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(Config.Timeouts.ExplicitWait));
    }

    [TearDown]
    public void Cleanup()
    {
        if (driver.Value != null)
        {
            driver.Value.Quit(); 
        }
    }
}