// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.IntegrationTests.Invoice.Infrastructure.Ui.GetInvoiceView;

public class GetInvoiceViewIntegrationTest : AbstractUiIntegrationTest
{
    public GetInvoiceViewIntegrationTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task GET_InvoiceExistsForId_ProvidesInvoiceData()
    {
        // given
        var invoice = CreateInvoice();

        // when
        var result = Get(invoice.Id);

        // then
        var content = await HtmlHelpers.GetDocumentAsync(result);
        Assert.Single(
            content.QuerySelectorAll("span")
                .Where(m => m.InnerHtml == invoice.BitPayId).ToList()
        );
        Assert.Single(
            content.QuerySelectorAll("span")
                .Where(m => m.InnerHtml == invoice.Status).ToList()
        );
        Assert.Single(
            content.QuerySelectorAll("span")
                .Where(m => m.InnerHtml == invoice.Price.ToString("#,###.00#"))
                .ToList()
        );
        Assert.Single(content.QuerySelectorAll("dd")
            .Where(m => m.InnerHtml == invoice.CreatedDate.ToUniversalTime().ToString("yyyy-MM-dd HH:mm UTC"))
            .ToList()
        );
        Assert.Single(
            content.QuerySelectorAll("dd")
                .Where(m => m.InnerHtml == invoice.BitPayOrderId).ToList()
        );
    }

    [Fact]
    public void GET_InvoiceNotExistsForId_RedirectTo404Page()
    {
        // given

        // when
        var result = Get(123456789);

        // then
        UnitTest.Equals("/404", result.Headers.Location!.ToString());
    }

    private HttpResponseMessage Get(long id)
    {
        return Get("/invoices/" + id).Result;
    }


    private CsharpKioskDemoDotnet.Invoice.Domain.Invoice CreateInvoice()
    {
        var invoiceJson = UnitTest.GetDataFromFile("invoice.json");
        var invoice = UnitTest.ToObject<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>(invoiceJson);
        GetInvoiceRepository().Save(invoice);

        return invoice;
    }
}