using MobileUITesting.DriverFactory;

namespace MobileUITesting.Configurations;

public class DeviceConfig
{
    public string DeviceName { get; set; }
    public string AppiumServerUrl { get; set; }
    public MobilePlatform Platform { get; set; }
    public string AppPath { get; set; }
    public string AutomationName { get; set; }
}
