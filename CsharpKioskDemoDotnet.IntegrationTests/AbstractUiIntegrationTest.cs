using System.Text;
using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CsharpKioskDemoDotnet.IntegrationTests;

public class AbstractUiIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>, IUnitTest
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    protected readonly Mock<IBitPayClient> bitPay = new();
    private readonly WebApplicationFactory<Program> webApplicationFactory;
    protected IUnitTest UnitTest => this;

    protected AbstractUiIntegrationTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        webApplicationFactory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IBitPayClient>(_ => bitPay.Object);
            });
        });
        _client = webApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
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
        var scope = webApplicationFactory.Services.CreateScope();
        var services = scope.ServiceProvider;
        return services.GetRequiredService<IInvoiceRepository>();
    }
}