using UITestFramework.Pages;

namespace UITestFramework.UITests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class TestPracticePage : BaseTest
{
    [Test]
    public void AddEntitytoFoodList()
    {
        var PracticePage = new PracticePage(Driver, Wait);
        
        Driver.Navigate().GoToUrl(Config.BaseUrl + PracticePage.Path);
        
        PracticePage.ClickAddButton();
        
        Assert.That(PracticePage.GetConfirmationMessage().Equals("Row 2 was added"), "Confirmation message should be 'Row 2 was added'");
    }
}