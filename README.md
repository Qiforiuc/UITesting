---

# Mobile UI Testing Framework

## Overview
This project is a **Mobile UI Testing Framework** built in **C#** using **Appium** and **NUnit**. It supports automated testing for both Android and iOS platforms, providing a structured and reusable approach to mobile application testing.

---

## Key Features
- **Cross-Platform Support**: Separate driver factories for Android and iOS.
- **Configurable Test Environment**: Device configurations and backend URLs are dynamically set.
- **Page Object Model (POM)**: Encapsulation of UI elements and actions for better maintainability.
- **Permission Handling**: Automatic permission granting for Android and iOS.
- **Test Reporting**: Integration with Allure for detailed test reports.

---

## Project Structure
### 1. `DriverFactory`
- **Purpose**: Creates platform-specific Appium drivers.
- **Classes**:
  - `AndroidDriverFactory`: Configures and initializes the Android driver.
  - `IOSDriverFactory`: Configures and initializes the iOS driver.

### 2. `Configurations`
- **Purpose**: Manages device and platform configurations.
- **Key Class**: `DeviceConfig` - Stores platform, device name, app path, and Appium server URL.

### 3. `Pages`
- **Purpose**: Implements the Page Object Model (POM) for UI interactions.
- **Example**: `LogoutPopUp` - Handles the logout confirmation popup for both Android and iOS.

### 4. `BaseTest`
- **Purpose**: Provides a base class for all test cases.
- **Responsibilities**:
  - Initializes the driver and WebDriverWait.
  - Manages test setup and teardown.
  - Provides utility methods for setting backend URLs.

---

## Key Classes and Methods
### `BaseTest`
- **`Setup()`**: Initializes the driver and test environment.
- **`TearDown()`**: Cleans up resources after each test.
- **`SetTestEnvironmentBackendURLs(LoginPage loginPage)`**: Configures backend URLs for testing.

### `AndroidDriverFactory`
- Configures Android-specific Appium options, such as `autoGrantPermissions` and `optionalIntentArguments`.

### `IOSDriverFactory`
- Configures iOS-specific Appium options, such as `autoAcceptAlerts` and `wdaLocalPort`.

### `LogoutPopUp`
- **`pressOnYesButton()`**: Clicks the "Yes" button on the logout confirmation popup.

---

## Configuration
### Device Configuration (`DeviceConfig`)
- **Fields**:
  - `Platform`: Specifies the platform (Android/iOS).
  - `DeviceName`: Name of the device.
  - `AppPath`: Path to the application under test.
  - `AppiumServerUrl`: URL of the Appium server.

### Example Configuration
```json
{
  "Devices": [
    {
      "Platform": "Android",
      "DeviceName": "emulator-5554",
      "AppiumServerUrl": "http://localhost:4723/",
      "AppPath": "maibank.apk",
      "AutomationName": "UiAutomator2",
      "PhoneNumber" : "060609975",
      "Idnp" : "0991006898381",
      "Pin5" : "11111"
    },
    {
      "Platform": "iOS",
      "DeviceName": "iPhone 15 Pro",
      "AppiumServerUrl": "http://localhost:4724/",
      "AppPath": "maibank.zip",
      "AutomationName": "XCUITest",
      "PhoneNumber" : "068846810",
      "Idnp" : "2004026057550",
      "Pin5" : "11111"
    }
  ]
}
```

---

## How to Run Tests
1. Configure devices in the configuration file.
2. Create test classes inheriting from `BaseTest`.
3. Use the `Setup()` method to initialize the driver.
4. Write test cases using the Page Object Model.
5. Execute tests using NUnit.

---

## Dependencies
- **Appium.WebDriver**: For mobile automation.
- **NUnit**: For test execution.
- **Selenium.Support**: For WebDriver utilities.
- **Allure.Commons**: For test reporting.

---

## Example Test Case
```csharp
[TestFixture]
public class LoginTests : BaseTest
{
    public LoginTests() : base(GetDeviceConfig(MobilePlatform.Android, "Pixel_4")) { }

    [Test]
    public void TestLogin()
    {
        var loginPage = new LoginPage(Driver, Wait);
        loginPage.EnterUsername("testuser");
        loginPage.EnterPassword("password");
        loginPage.PressLoginButton();

        Assert.IsTrue(loginPage.IsLoginSuccessful());
    }
}
```
