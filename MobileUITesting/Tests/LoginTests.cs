using Allure.NUnit;
using MobileUITesting.Configurations;
using MobileUITesting.DriverFactory;
using MobileUITesting.POMs;
using MobileUITesting.TestDataFactory; // Correct namespace
using NUnit.Allure.Attributes;  // For attributes like [AllureSeverity]
using NUnit.Framework;
using SeverityLevel = Allure.Net.Commons.SeverityLevel;

namespace MobileUITesting.Tests;
[TestFixture(MobilePlatform.Android, "emulator-5554")]
[TestFixture(MobilePlatform.iOS, "iPhone 15 Pro")]
[Parallelizable(ParallelScope.All)]
[AllureNUnit]
public class LoginTests:BaseTest
{
    private TermsAndConditionsPage _termsAndConditionsPage;
    private LoginPage _loginPage;
    private PhoneNumberPage _phoneNumberPage;
    private OtpPage _otpPage;
    private ConfirmationTypePage _confirmationTypePage;
    private IdnpPage _idnpPage;
    private Pin5Page _pin5Page;
    private MiaPopUp _miaPopUp;
    private MaibCallCenterWarningPopUp _maibCallCenterWarningPopUp;
    private HomePage _homePage;
    private NavigationMenu _navigationMenu;
    private MorePage _morePage;
    private LogoutPopUp _logoutPopUp;
    
    private readonly DeviceConfig _deviceConfig;
    private User _user;

    public LoginTests(MobilePlatform platform, string deviceName)
        : base(GetDeviceConfig(platform, deviceName))
    {
        _deviceConfig = GetDeviceConfig(platform, deviceName);
    }

    [SetUp]
    public void SetUpTest()
    {
        base.Setup();
        _termsAndConditionsPage = new TermsAndConditionsPage(Driver, Wait, _deviceConfig);
        _loginPage = new LoginPage(Driver, Wait, _deviceConfig);
        _phoneNumberPage = new PhoneNumberPage(Driver, Wait, _deviceConfig);
        _otpPage = new OtpPage(Driver, Wait, _deviceConfig);
        _confirmationTypePage = new ConfirmationTypePage(Driver, Wait, _deviceConfig);
        _idnpPage = new IdnpPage(Driver, Wait, _deviceConfig);
        _pin5Page = new Pin5Page(Driver, Wait, _deviceConfig);
        _maibCallCenterWarningPopUp = new MaibCallCenterWarningPopUp(Driver, Wait, _deviceConfig);
        _homePage = new HomePage(Driver, Wait, _deviceConfig);
        _miaPopUp = new MiaPopUp(Driver, Wait, _deviceConfig);
        _navigationMenu = new NavigationMenu(Driver, Wait, _deviceConfig);
        _morePage = new MorePage(Driver, Wait, _deviceConfig);
        _logoutPopUp = new LogoutPopUp(Driver, Wait, _deviceConfig);
        
        _user = new User(_deviceConfig);
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


    [Test(Description = $"Set Test Environment Backend URLs")]
    [AllureTag("Smoke")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureOwner("Adrian")]
    public void LoginByIdnp()
    {
        if (_deviceConfig.Platform.Equals(MobilePlatform.Android) && _termsAndConditionsPage.isOnCorrectScreen())
        {
            _termsAndConditionsPage.pressOnAcceptButton();
        }
        
        SetTestEnvironmentBackendURLs(_loginPage);
        
        Assert.That(_loginPage.isOnCorrectScreen, "The user must be on Login Page");
        _loginPage.pressLoginButton();
        
        Assert.That(_phoneNumberPage.isOnCorrectScreen, "The user must be on Phone Number Page");
        _phoneNumberPage.inputPhoneNumber(_user.PhoneNumber);
        _phoneNumberPage.pressOnContinueButton();
        
        Assert.That(_otpPage.isOnCorrectScreen, "The user must be on OTP Page");
        _otpPage.inputOtp("123456");
        
        Assert.That(_confirmationTypePage.isOnCorrectScreen, "The user must be on Confirmation Type Page");
        _confirmationTypePage.pressOnLoginWithoutBiometricsButton();
        
        Assert.That(_idnpPage.isOnCorrectScreen, "The user must be on Login Page");
        _idnpPage.inputIdnp(_user.Idnp);
        _idnpPage.pressOnNextButton();
        
        Assert.That(_pin5Page.isOnCorrectScreen, "The user must be on Pin5 Page");
        _pin5Page.enterPin(_deviceConfig.Pin5);

        if (_deviceConfig.Platform.Equals(MobilePlatform.Android) && _maibCallCenterWarningPopUp.isOnCorrectScreen())
        {
            _maibCallCenterWarningPopUp.pressOnYesButton();
        }
        
        if (_deviceConfig.Platform.Equals(MobilePlatform.Android) && _miaPopUp.isOnCorrectScreen())
        {
            _miaPopUp.pressOnCloseButton();
        }
        
        Assert.That(_homePage.isOnCorrectScreen, "The user must be on Home Page");
    }
    
    [TearDown]
    public void afterTest()
    {
        _navigationMenu.pressOnMoreButton();
        _morePage.pressOnLogoutButton();
        _logoutPopUp.pressOnYesButton();
    }
}