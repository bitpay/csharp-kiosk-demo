// Copyright 2023 BitPay.
// All rights reserved.

using System.Globalization;

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
        var price = Convert.ToDecimal(validatedParams["price"], CultureInfo.CurrentCulture);
        var posData = _objectToJsonConverter.Execute(validatedParams);
        var invoice = new BitPay.Models.Invoice.Invoice(price, _bitPayProperties.Currency)
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
            throw ae.InnerException!;
        }
    }
}