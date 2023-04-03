using BitPaySDK;
using CsharpKioskDemoDotnet.Shared;
using CsharpKioskDemoDotnet.Shared.BitPayProperties;
using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class CreateBitPayInvoice
{
    private readonly BitPay _bitPayClient;
    private readonly BitPayProperties _bitPayProperties;
    private readonly IObjectToJsonConverter _objectToJsonConverter;
    private readonly IGetNotificationUrl _getNotificationUrl;

    public CreateBitPayInvoice(
        BitPay bitPayClient,
        IOptions<BitPayProperties> bitPayPropertiesOption,
        IObjectToJsonConverter objectToJsonConverter,
        IGetNotificationUrl getNotificationUrl
    )
    {
        _bitPayClient = bitPayClient;
        _bitPayProperties = bitPayPropertiesOption.Value;
        _objectToJsonConverter = objectToJsonConverter;
        _getNotificationUrl = getNotificationUrl;
    }

    internal BitPaySDK.Models.Invoice.Invoice Execute(
        Dictionary<string, string> validatedParams,
        string uuid
    ) {
        var price = Convert.ToDouble(validatedParams["price"]);
        var posData = _objectToJsonConverter.Execute(validatedParams);
        var invoice = new BitPaySDK.Models.Invoice.Invoice(price, _bitPayProperties.GetCurrency())
        {
            OrderId = Guid.NewGuid().ToString(),
            NotificationEmail = _bitPayProperties.NotificationEmail,
            TransactionSpeed = "medium",
            ItemDesc = "Example",
            PosData = posData,
            NotificationUrl = _getNotificationUrl.Execute(uuid),
            ExtendedNotifications = true
        };

        try
        {
            var createInvoiceTask = _bitPayClient.CreateInvoice(invoice);
            createInvoiceTask.Wait();
            return createInvoiceTask.Result;
        }
        catch (AggregateException ae)
        {
            throw ae.InnerException;
        }
    }
}