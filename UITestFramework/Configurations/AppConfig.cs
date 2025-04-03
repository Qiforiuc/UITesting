namespace UITestFramework.Configurations;

public class AppConfig
{
    public string Environment { get; set; }
    public string BaseUrl { get; set; }
    public string Browser { get; set; }
    public bool Headless { get; set; }
    public TimeoutsConfig Timeouts { get; set; }
    public CredentialsConfig Credentials { get; set; }
    public string ScreenshotPath { get; set; }
    public string LogLvel { get; set; }
}

public class TimeoutsConfig
{
    public int ImplicitWait { get; set; }
    public int ExplicitWait { get; set; }
    public int PageLoad { get; set; }
}