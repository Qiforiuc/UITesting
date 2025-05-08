using ApiTesting.Commons;

namespace ApiTesting.Base;

public class BaseTest
{
    public readonly Context instance = Context.Instance;

    [OneTimeSetUp]
    public void SetUp()
    {
        //setup needed for the tests
    }
}