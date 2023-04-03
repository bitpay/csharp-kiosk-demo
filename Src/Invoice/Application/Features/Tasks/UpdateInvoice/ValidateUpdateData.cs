using BitPaySDK.Models.Invoice;
using CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;
using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;

public class ValidateUpdateData
{
    private readonly string[] _allowedStatuses =
    {
        Status.New,
        Status.Paid,
        Status.Confirmed,
        Status.Complete,
        Status.Expired,
        Status.Invalid,
    };

    private readonly string[] _allowedExceptionStatus =
    {
        "false",
        "paidPartial",
        "paidOver",
    };

    private readonly IJsonToObjectConverter _jsonToObjectConverter;

    public ValidateUpdateData(IJsonToObjectConverter jsonToObjectConverter)
    {
        _jsonToObjectConverter = jsonToObjectConverter;
    }

    internal void Execute(
        Dictionary<string, object?> updateData,
        Domain.Invoice invoice
    )
    {
        var errors = new Dictionary<string, string>();

        ValidateId(updateData, invoice.BitPayId, errors);
        ValidateDouble(updateData, "price", errors);
        ValidateString(updateData, "currency", 10, errors);
        ValidateStatus(updateData, errors);
        ValidateString(updateData, "orderId", 255, errors);
        ValidateExceptionStatus(updateData, errors);
        ValidateString(updateData, "url", 255, errors);
        ValidateDouble(updateData, "amountPaid", errors);
        ValidateString(updateData, "transactionCurrency", 10, errors);
        ValidateExpirationTime(updateData, errors);
        ValidateBuyerFields(updateData, errors);

        if (errors.Any())
        {
            throw new ValidationInvoiceUpdateDataFailed(errors);
        }
    }

    private void ValidateBuyerFields(
        Dictionary<string, object?> updateData,
        Dictionary<string, string> errors
    )
    {
        if (!updateData.ContainsKey("buyerFields") || updateData["buyerFields"] == null)
        {
            return;
        }

        Dictionary<string,object?> buyerFields;
        
        try
        {
            buyerFields = _jsonToObjectConverter.Execute<Dictionary<string, object?>>(
                updateData["buyerFields"]!.ToString()
            )!;
        }
        catch (Exception exception)
        {
            errors.Add("buyerFields", "BuyerFields isn't object.");
            return;
        }

        ValidateString(buyerFields, "buyerName", 255, errors);
        ValidateString(buyerFields, "buyerAddress1", 255, errors);
        ValidateString(buyerFields, "buyerAddress2", 255, errors);
        ValidateString(buyerFields, "buyerCity", 255, errors);
        ValidateString(buyerFields, "buyerState", 255, errors);
        ValidateString(buyerFields, "buyerZip", 255, errors);
        ValidateString(buyerFields, "buyerCountry", 2, errors);
        ValidateString(buyerFields, "buyerPhone", 255, errors);
        ValidateString(buyerFields, "buyerEmail", 255, errors);
        ValidateBoolean(buyerFields, "buyerNotify", errors);
    }

    private void ValidateBoolean(
        Dictionary<string, object?> buyerFields,
        string fieldName,
        Dictionary<string, string> errors
    )
    {
        if (!buyerFields.ContainsKey(fieldName) || buyerFields[fieldName] == null)
        {
            return;
        }

        try
        {
            bool.Parse(buyerFields[fieldName]!.ToString()!);
        }
        catch (FormatException)
        {
            errors.Add("buyerNotify", "BuyerNotify is not boolean.");
        }
    }

    private void ValidateExpirationTime(
        Dictionary<string, object?> updateData,
        Dictionary<string, string> errors
    )
    {
        if (!updateData.ContainsKey("expirationTime") || updateData["expirationTime"] == null)
        {
            return;
        }

        try
        {
            long.Parse(updateData["expirationTime"]!.ToString()!);
        }
        catch (FormatException)
        {
            errors.Add("expirationTime", "ExpirationTime is not number.");
        }
    }

    private void ValidateDouble(
        Dictionary<string, object?> updateData,
        string fieldName,
        Dictionary<string, string> errors
    )
    {
        if (!updateData.ContainsKey(fieldName) || updateData[fieldName] == null)
        {
            return;
        }

        try
        {
            double.Parse(updateData[fieldName]!.ToString()!);
        }
        catch (FormatException)
        {
            errors.Add(fieldName, $"{Capitalize(fieldName)} is not number.");
        }
    }

    private void ValidateId(
        Dictionary<string, object?> updateData,
        string bitPayId,
        Dictionary<string, string> errors
    )
    {
        if (!updateData.ContainsKey("id") || updateData["id"] == null)
        {
            errors.Add("id", "Id is empty.");
            return;
        }


        if (updateData["id"]!.GetType() != typeof(string))
        {
            errors.Add("id", "Id isn't text.");
            return;
        }

        if ((string)updateData["id"]! != bitPayId)
        {
            errors.Add("id", "Id not equal.");
        }
    }

    private void ValidateExceptionStatus(
        Dictionary<string, object?> updateData,
        Dictionary<string, string> errors
    )
    {
        if (!updateData.ContainsKey("ExceptionStatus"))
        {
            return;
        }

        if (!_allowedExceptionStatus.Contains(updateData["exceptionStatus"]))
        {
            errors.Add("exceptionStatus", "ExceptionStatus has wrong type.");
        }
    }

    private void ValidateStatus(
        Dictionary<string, object?> updateData,
        Dictionary<string, string> errors
    )
    {
        if (!updateData.ContainsKey("status"))
        {
            errors.Add("status", "Status is empty.");
            return;
        }

        if (!_allowedStatuses.Contains(updateData["status"]))
        {
            errors.Add("status", "Status has wrong type.");
        }
    }

    private void ValidateString(
        Dictionary<string, object?> updateData,
        string fieldName,
        int maxLength,
        Dictionary<string, string> errors
    )
    {
        if (!updateData.ContainsKey(fieldName) || updateData[fieldName] == null)
        {
            return;
        }

        if (updateData[fieldName]!.GetType() != typeof(string))
        {
            errors.Add(fieldName, Capitalize(fieldName) + " isn't text.");
            return;
        }

        if (((string)updateData[fieldName]!).Length > maxLength)
        {
            errors.Add(fieldName, Capitalize(fieldName) + " is too long.");
        }
    }

    private static string Capitalize(string text)
    {
        return string.Concat(text[..1].ToUpper(), text.AsSpan(1));
    }
}