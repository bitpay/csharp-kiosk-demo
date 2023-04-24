// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Infrastructure.Features.Tasks.CreateInvoice;

using Microsoft.Extensions.Configuration;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Infrastructure.Features.Tasks.CreateInvoice;

public class GetNotificationUrlTest : IUnitTest
{
    private IUnitTest UnitTest => this;

    [Fact]
    void Execute_InvoiceUuId_ReturnNotificationUrl()
    {
        // given
        var invoiceUuid = "123-456-789";

        // when
        var result = GetTestedClass().Execute(invoiceUuid);

        // then
        UnitTest.Equals("http://localhost/invoices/123-456-789", result);
    }

    private GetNotificationUrl GetTestedClass()
    {
        var configuration = GetConfiguration();

        return new GetNotificationUrl(configuration);
    }

    private IConfiguration GetConfiguration()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            { "AppUrl", "http://localhost" },
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        return configuration;
    }
}