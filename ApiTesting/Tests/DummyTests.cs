using ApiTesting.Base;
using ApiTesting.ApiClient;
using ApiTesting.Commons;
using ApiTesting.Pojo;

namespace ApiTesting.Tests;

public class DummyTests:BaseTest
{
    private Client _client;
    private Context _context = Context.Instance;
    
    [SetUp]
    public void Setup()
    {
        _client = new Client(_context.GetValue("ReqresUri").ToString());
    }

    [Test]
    public void Test1()
    {
        var response = _client.GetRequest<UserResponse>("/api/users/2");
        Assert.That(response.Data.First_Name, Is.EqualTo("Janet"), "First name should be Janet");
    }
}