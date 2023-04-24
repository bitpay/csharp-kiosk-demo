// Copyright 2023 BitPay.
// All rights reserved.

using System.Globalization;

using CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;
using CsharpKioskDemoDotnet.Invoice.Domain.Buyer;
using CsharpKioskDemoDotnet.Invoice.Domain.Payment;
using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;

public class GetInvoiceWithUpdateData
{

    private readonly IJsonToObjectConverter _jsonToObjectConverter;

    public GetInvoiceWithUpdateData(IJsonToObjectConverter jsonToObjectConverter)
    {
        _jsonToObjectConverter = jsonToObjectConverter;
    }

    public Domain.Invoice Execute(
        Dictionary<string, object?> updateData,
        Domain.Invoice invoice
    )
    {
        ArgumentNullException.ThrowIfNull(updateData);
        ArgumentNullException.ThrowIfNull(invoice);

        return new Domain.Invoice(
            uuid: invoice.Uuid,
            posData: invoice.PosData,
            price: GetFieldValue("price", updateData, invoice.Price),
            currencyCode: GetFieldValue("currency", updateData, invoice.CurrencyCode)!,
            bitPayId: invoice.BitPayId,
            status: GetFieldValue("status", updateData, invoice.Status)!,
            createdDate: invoice.CreatedDate,
            transactionSpeed: invoice.TransactionSpeed,
            invoicePayment: GetPayment(updateData, invoice.InvoicePayment),
            invoiceBuyer: GetBuyer(invoice.InvoiceBuyer, updateData),
            invoiceRefund: invoice.InvoiceRefund
        )
        {
            ExpirationTime = GetExpirationTime(updateData, invoice),
            ExceptionStatus = GetFieldValue("exceptionStatus", updateData, invoice.ExceptionStatus),
            BitPayUrl = GetFieldValue("url", updateData, invoice.BitPayUrl),
        };
    }

    private InvoiceBuyer GetBuyer(
        InvoiceBuyer invoiceBuyer,
        Dictionary<string, object?> updateData
    )
    {
        var buyerFieldsData = updateData["buyerFields"];

        if (buyerFieldsData == null)
        {
            return invoiceBuyer;
        }

        var buyerFields = _jsonToObjectConverter.Execute<Dictionary<string, object?>>(buyerFieldsData.ToString())!;

        return new InvoiceBuyer(invoiceBuyer.InvoiceBuyerProvidedInfo)
        {
            Name = GetFieldValue("buyerName", buyerFields, invoiceBuyer.Name),
            Address1 = GetFieldValue("buyerAddress1", buyerFields, invoiceBuyer.Address1),
            Address2 = GetFieldValue("buyerAddress2", buyerFields, invoiceBuyer.Address2),
            City = GetFieldValue("buyerCity", buyerFields, invoiceBuyer.City),
            Region = GetFieldValue("buyerState", buyerFields, invoiceBuyer.Region),
            PostalCode = GetFieldValue("buyerZip", buyerFields, invoiceBuyer.PostalCode),
            Country = GetFieldValue("buyerCountry", buyerFields, invoiceBuyer.Country),
            Email = GetFieldValue("buyerEmail", buyerFields, invoiceBuyer.Email),
            Phone = GetFieldValue("buyerPhone", buyerFields, invoiceBuyer.Phone),
            Notify = GetFieldValue("buyerNotify", buyerFields, invoiceBuyer.Notify)
        };
    }

    private DateTime? GetExpirationTime(
        Dictionary<string, object?> updateData,
        Domain.Invoice invoice
    )
    {
        if (!updateData.ContainsKey("expirationTime"))
        {
            return invoice.ExpirationTime;
        }

        var expirationTime = updateData["expirationTime"];
        if (expirationTime == null)
        {
            return null;
        }

        return ParseMillisecondsToDataTime.Execute(expirationTime.ToString()!);
    }

    private InvoicePayment GetPayment(
        Dictionary<string, object?> updateData,
        InvoicePayment invoicePayment
    )
    {
        return new InvoicePayment
        {
            AmountPaid = GetFieldValue("amountPaid", updateData, invoicePayment.AmountPaid),
            TransactionCurrency = GetFieldValue("transactionCurrency", updateData, invoicePayment.TransactionCurrency),
        };
    }

    private T? GetFieldValue<T>(
        string fieldName,
        Dictionary<string, object?> updateData,
        T? defaultValue
    )
    {
        if (!updateData.ContainsKey(fieldName))
        {
            return defaultValue;
        }

        var value = updateData[fieldName];

        if (value == null)
        {
            return default;
        }

        var type = typeof(T);
        var nullableType = Nullable.GetUnderlyingType(type);

        if (nullableType == null)
        {
            return (T)Convert.ChangeType(value, type, CultureInfo.CurrentCulture);
        }

        return (T)Convert.ChangeType(value.ToString(), nullableType, CultureInfo.CurrentCulture)!;
    }
}