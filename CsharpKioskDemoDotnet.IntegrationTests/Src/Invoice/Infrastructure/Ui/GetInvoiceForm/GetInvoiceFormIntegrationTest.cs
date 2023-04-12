namespace CsharpKioskDemoDotnet.IntegrationTests.Invoice.Infrastructure.Ui.GetInvoiceForm;

public class GetInvoiceFormIntegrationTest : AbstractUiIntegrationTest
{
    public GetInvoiceFormIntegrationTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task GET_FormFieldsLoadedFromYamlFile_ProvidesAllFieldsInForm()
    {
        // given
     
        // when
        var result = await Get("/");
        
        // then
        var content = await HtmlHelpers.GetDocumentAsync(result);
        Assert.NotNull(content.QuerySelector("select[name='store']"));
        UnitTest.Equals(2, content.QuerySelectorAll("input[name='register']").Length);
        Assert.NotNull(content.QuerySelector("input[name='reg_transaction_no']"));
        Assert.NotNull(content.QuerySelector("input[name='price']"));
    }
}