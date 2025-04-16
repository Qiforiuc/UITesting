using MobileUITesting.Configurations;

namespace MobileUITesting.TestDataFactory;

public class User
{
    public string PhoneNumber { get; set; }
    public string Idnp { get; set; }
    public string Pin5 { get; set; }
    
    public User(DeviceConfig deviceConfig)
    {
        PhoneNumber = deviceConfig.PhoneNumber;
        Idnp = deviceConfig.Idnp;
        Pin5 = deviceConfig.Pin5;
    }
}