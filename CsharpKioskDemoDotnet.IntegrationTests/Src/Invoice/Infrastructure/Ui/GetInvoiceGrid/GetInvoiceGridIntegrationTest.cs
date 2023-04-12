namespace CsharpKioskDemoDotnet.IntegrationTests.Invoice.Infrastructure.Ui.GetInvoiceGrid;

public class GetInvoiceGridIntegrationTest : AbstractUiIntegrationTest
{
    public GetInvoiceGridIntegrationTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task GET_2InvoicesAreExistsAndPageIs1_Provides2ItemInGrid()
    {
        // given
        CreateInvoice();
        CreateInvoice();
        
        // when
        var result = await Get("/invoices?page=1");
        
        // then
        var content = await HtmlHelpers.GetDocumentAsync(result);
        UnitTest.Equals(2, content.QuerySelectorAll("td.status").Length);
    }
    
    [Fact]
    public async Task GET_2InvoicesAreExistsAndPageIs2_Provides0ItemInGrid()
    {
        // given
        CreateInvoice();
        CreateInvoice();
        
        // when
        var result = await Get("/invoices?page=2");
        
        // then
        var content = await HtmlHelpers.GetDocumentAsync(result);
        Assert.Empty(content.QuerySelectorAll("td.status"));
    }
    
    private void CreateInvoice() {
        var invoiceJson = UnitTest.GetDataFromFile("invoice.json");
        var invoice = UnitTest.ToObject<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>(invoiceJson);
        GetInvoiceRepository().Save(invoice);
    }
}