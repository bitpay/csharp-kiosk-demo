// Copyright 2023 BitPay.
// All rights reserved.

using System.Text;

using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Shared;
using CsharpKioskDemoDotnet.Shared.Logger;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Moq;

namespace CsharpKioskDemoDotnet.IntegrationTests;

public class AbstractUiIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>, IUnitTest
{
    protected IUnitTest UnitTest => this;
    protected readonly Mock<IBitPayClient> BitPay = new();

    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    protected AbstractUiIntegrationTest(CustomWebApplicationFactory<Program> factory)
    {
        _webApplicationFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IBitPayClient>(_ => BitPay.Object);
                services.AddSingleton<ILogger, FakeLogger>();
            });
        });
        _client = _webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    protected Task<HttpResponseMessage> Get(string url)
    {
        return _client.GetAsync(url);
    }

    protected Task<HttpResponseMessage> Post(
        string url,
        string jsonRequest
    )
    {
        var httpContent = new StringContent(
            jsonRequest,
            Encoding.UTF8,
            "application/json"
        );

        return _client.PostAsync(url, httpContent);
    }

    protected Task<HttpResponseMessage> PostForm(
        string url,
        Dictionary<string, string> request
    )
    {
        return _client.PostAsync(url, new FormUrlEncodedContent(request));
    }

    protected IInvoiceRepository GetInvoiceRepository()
    {
        var scope = _webApplicationFactory.Services.CreateScope();
        var services = scope.ServiceProvider;
        return services.GetRequiredService<IInvoiceRepository>();
    }
}

public class FakeLogger : ILogger
{
    public void Info(LogCode code, string message, Dictionary<string, object?> context)
    {
    }

    public void Error(LogCode code, string message, Dictionary<string, object?> context)
    {
    }
}