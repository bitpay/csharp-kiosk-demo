// Copyright 2023 BitPay.
// All rights reserved.

using System.Globalization;

using BitPay.Models.Invoice;

using CsharpKioskDemoDotnet.Shared;
using CsharpKioskDemoDotnet.Shared.BitPayProperties;

using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class CreateBitPayInvoice
{
    private readonly IBitPayClient _bitPayClient;
    private readonly BitPayProperties _bitPayProperties;
    private readonly IObjectToJsonConverter _objectToJsonConverter;
    private readonly IGetNotificationUrl _getNotificationUrl;

    public CreateBitPayInvoice(
        IBitPayClient bitPayClient,
        IOptions<BitPayProperties> bitPayPropertiesOption,
        IObjectToJsonConverter objectToJsonConverter,
        IGetNotificationUrl getNotificationUrl
    )
    {
        ArgumentNullException.ThrowIfNull(bitPayClient);
        ArgumentNullException.ThrowIfNull(bitPayPropertiesOption);
        ArgumentNullException.ThrowIfNull(objectToJsonConverter);
        ArgumentNullException.ThrowIfNull(getNotificationUrl);

        _bitPayClient = bitPayClient;
        _bitPayProperties = bitPayPropertiesOption.Value;
        _objectToJsonConverter = objectToJsonConverter;
        _getNotificationUrl = getNotificationUrl;
    }

    internal BitPay.Models.Invoice.Invoice Execute(
        Dictionary<string, string> validatedParams,
        string uuid
    )
    {
        ArgumentNullException.ThrowIfNull(validatedParams);
        ArgumentNullException.ThrowIfNull(uuid);

        var price = Convert.ToDecimal(validatedParams["price"], CultureInfo.CurrentCulture);
        var posData = _objectToJsonConverter.Execute(validatedParams);
        var invoice = new BitPay.Models.Invoice.Invoice(price, _bitPayProperties.Currency)
        {
            OrderId = Guid.NewGuid().ToString(),
            NotificationEmail = _bitPayProperties.NotificationEmail,
            TransactionSpeed = "medium",
            ItemDesc = string.Concat(
                _bitPayProperties.Mode[..1].ToUpper(CultureInfo.CurrentCulture),
                _bitPayProperties.Mode.AsSpan(1)
            ),
            PosData = posData,
            NotificationUrl = _getNotificationUrl.Execute(uuid),
            ExtendedNotifications = true
        };

        if (validatedParams.Keys.Where( key => key.StartsWith("buyer", StringComparison.Ordinal)).Any()) {
            invoice.Buyer = new Buyer() {
                Name = validatedParams!.GetValueOrDefault("buyerName", null),
                Address1 = validatedParams!.GetValueOrDefault("buyerAddress1", null),
                Address2 = validatedParams!.GetValueOrDefault("buyerAddress2", null),
                Locality = validatedParams!.GetValueOrDefault("buyerLocality", null),
                Region = validatedParams!.GetValueOrDefault("buyerRegion", null),
                PostalCode = validatedParams!.GetValueOrDefault("buyerPostalCode", null),
                Country = "US",
                Email = validatedParams!.GetValueOrDefault("buyerEmail", null),
                Phone = validatedParams!.GetValueOrDefault("buyerPhone", null),
            };
        }

        try
        {
            var createInvoiceTask = _bitPayClient.CreateInvoice(invoice);
            createInvoiceTask.Wait();
            return createInvoiceTask.Result;
        }
        catch (AggregateException ae)
        {
            throw ae.InnerException!;
        }
    }
}