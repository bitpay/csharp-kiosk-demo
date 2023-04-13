using System.Net;
using BitPay.Exceptions;
using Moq;

namespace CsharpKioskDemoDotnet.IntegrationTests.Invoice.Infrastructure.Ui.CreateInvoice;

public class CreateInvoiceIntegrationTest : AbstractUiIntegrationTest
{
    private const string Url = "/invoice";
    
    public CreateInvoiceIntegrationTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public void POST_BitPayInvoiceCreated_SaveInvoiceInDatabaseAndRedirectToBitPayUrl()
    {
        // given
        bitPay.Setup(e => e.CreateInvoice(It.IsAny<BitPay.Models.Invoice.Invoice>()))
            .ReturnsAsync(UnitTest.GetBitPayInvoice());

        // when
        var result = Post(
            new Dictionary<string, string>
            {
                { "store", "store-1" },
                { "register", "2" },
                { "reg_transaction_no", "test" },
                { "price", "123" }
            }
        );

        // then
        UnitTest.Equals(HttpStatusCode.MovedPermanently, result.StatusCode);
        Assert.Single(GetInvoiceRepository().FindAll());
    }

    [Fact]
    public void POST_CreatingBitPayInvoiceThrowException_DoNotSaveInvoiceInDatabase()
    {
        // given
        bitPay.Setup(e => e.CreateInvoice(It.IsAny<BitPay.Models.Invoice.Invoice>()))
            .Throws(new AggregateException(new InvoiceCreationException()));

        // when
        var result = Post(
            new Dictionary<string, string>
            {
                { "store", "store-1" },
                { "register", "2" },
                { "reg_transaction_no", "test" },
                { "price", "123" }
            }
        );
        
        // then
        Assert.Empty(GetInvoiceRepository().FindAll());
    }
    
    [Fact]
    public void POST_RequestMissingRequiredFields_DoNotSaveInvoiceInDatabase()
    {
        // given

        // when
        var result = Post(
            new Dictionary<string, string>
            {
                { "store", "store-1" },
                { "register", "2" },
                { "reg_transaction_no", "test" }
            }
        );
    
        // then
        Assert.Empty(GetInvoiceRepository().FindAll());
    }

    private HttpResponseMessage Post(Dictionary<string, string> request)
    {
        return PostForm(Url, request).Result;
    }
}