// Copyright 2023 BitPay.
// All rights reserved.

using System.Net;

namespace CsharpKioskDemoDotnet.IntegrationTests.Invoice.Infrastructure.Ui.UpdateInvoice;

public class UpdateInvoiceIntegrationTest : AbstractUiIntegrationTest
{
    public UpdateInvoiceIntegrationTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task POST_InvoiceExistsForUuidAndUpdateDataAreValid_UpdateInvoice()
    {
        // given
        var invoice = CreateInvoice();
        var updateDataJson = UnitTest.GetDataFromFile("updateData.json");

        // when
        var result = await Post(
            "/invoices/" + invoice.Uuid,
            updateDataJson,
            new Dictionary<string, string>
            {
                { "x-signature", "bKGK0WgsFfMSEg4fpik9+OdjYrYNA1E99kI1QJmbfKw=" }
            }
        );

        // then
        result.EnsureSuccessStatusCode();
        UnitTest.Equals(
            "expired",
            GetInvoiceRepository().FindById(invoice.Id).Status
        );
    }

    [Fact]
    public async Task POST_InvoiceDoesNotExistsForUuid_DoNotUpdateInvoice()
    {
        // given
        var invoice = CreateInvoice();
        var updateDataJson = UnitTest.GetDataFromFile("updateData.json");

        // when
        var result = await Post(
            "/invoices/12312412", 
            updateDataJson,
            new Dictionary<string, string>
            {
                { "x-signature", "bKGK0WgsFfMSEg4fpik9+OdjYrYNA1E99kI1QJmbfKw=" }
            }
        );

        // then
        UnitTest.Equals(
            HttpStatusCode.NotFound,
            result.StatusCode
        );
        UnitTest.Equals(
            "new",
            GetInvoiceRepository().FindById(invoice.Id).Status
        );
    }

    [Fact]
    public async Task POST_UpdateDataAreInvalid_DoNotUpdateInvoice()
    {
        // given
        var invoice = CreateInvoice();
        var updateDataJson = UnitTest.GetDataFromFile("invalidUpdateData.json");

        // when
        var result = await Post(
            "/invoices/" + invoice.Uuid, 
            updateDataJson, 
            new Dictionary<string, string>
            {
                { "x-signature", "16imUAXdJqur7yyQyDRRfcbPCeMPiuBFnNJVLlpi3hQ=" }
            }
        );

        // then
        UnitTest.Equals(
            HttpStatusCode.BadRequest,
            result.StatusCode
        );
        UnitTest.EqualsJson(
            UnitTest.GetDataFromFile("invalidUpdateDataResponse.json"),
            result.Content.ReadAsStringAsync().Result
        );
        UnitTest.Equals(
            "new",
            GetInvoiceRepository().FindById(invoice.Id).Status
        );
    }

    [Fact]
    public async Task POST_WebhookSignatureInvalid_DoNotUpdateInvoice()
    {
        // given
        var invoice = CreateInvoice();
        var updateDataJson = UnitTest.GetDataFromFile("invalidUpdateData.json");

        // when
        var result = await Post(
            "/invoices/" + invoice.Uuid, 
            updateDataJson, 
            new Dictionary<string, string>
            {
                { "x-signature", "randomsignature" }
            }
        );

        // then
        result.EnsureSuccessStatusCode();
        UnitTest.Equals(
            "new",
            GetInvoiceRepository().FindById(invoice.Id).Status
        );
    }

    private CsharpKioskDemoDotnet.Invoice.Domain.Invoice CreateInvoice()
    {
        var invoiceJson = UnitTest.GetDataFromFile("invoice.json");
        var invoice = UnitTest.ToObject<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>(invoiceJson);
        GetInvoiceRepository().Save(invoice);

        return invoice;
    }
}
