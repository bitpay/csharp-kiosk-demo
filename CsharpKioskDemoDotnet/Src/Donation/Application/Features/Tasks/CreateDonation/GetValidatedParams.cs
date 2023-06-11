// Copyright 2023 BitPay.
// All rights reserved.

using GetValidatedPosParams = CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice.GetValidatedParams;
using CsharpKioskDemoDotnet.Shared.BitPayProperties;
using CsharpKioskDemoDotnet.Shared.Form;

using Microsoft.Extensions.Options;
using System.Globalization;

namespace CsharpKioskDemoDotnet.Donation.Application.Features.Tasks.CreateDonation;

public class GetValidatedParams : IGetValidatedParams
{
    private static List<string> _allowedRegions = new() {
        "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS",
        "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC",
        "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
    };

    private readonly decimal _maxPrice;
    private readonly BitPayProperties _bitPayProperties;
    private readonly GetValidatedPosParams _getValidatedPosParams;
    private readonly EmailValidator _emailValidator = new EmailValidator();
    private readonly ZipCodeValidator _zipCodeValidator = new ZipCodeValidator();

    public GetValidatedParams(IOptions<BitPayProperties> bitPayPropertiesOption)
    {
        ArgumentNullException.ThrowIfNull(bitPayPropertiesOption);
        _bitPayProperties = bitPayPropertiesOption.Value;
        _getValidatedPosParams = new GetValidatedPosParams(bitPayPropertiesOption);
        _maxPrice = _bitPayProperties.MaxPrice;
    }

    public Dictionary<string, string> Execute(Dictionary<string, string?> requestParameters)
    {
        var validatedParams = new Dictionary<string, string>();

        AddValidatedText(requestParameters, "buyerName", validatedParams);
        AddValidatedText(requestParameters, "buyerAddress1", validatedParams);
        AddValidatedText(requestParameters, "buyerLocality", validatedParams);
        AddValidatedText(requestParameters, "buyerPostalCode", validatedParams, _zipCodeValidator);
        AddValidatedText(requestParameters, "buyerPhone", validatedParams);
        AddValidatedText(requestParameters, "buyerEmail", validatedParams, _emailValidator);
        AddValidatedSelect(requestParameters, "buyerRegion", validatedParams, _allowedRegions);
        AddValidatedPrice(requestParameters: requestParameters, validatedParams: validatedParams);

        var address2 = requestParameters.GetValueOrDefault("buyerAddress2", null);
        if (address2 != null) {
            validatedParams.Add("buyerAddress2", address2);
        }

        var validatedPosDataParams = _getValidatedPosParams.Execute(requestParameters);
        foreach (var keyValuePair in validatedPosDataParams)
        {
            if (!validatedParams.ContainsKey(keyValuePair.Key))
            {
                validatedParams.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        return validatedParams;
    }

    private void AddValidatedPrice(
        Dictionary<string, string?> requestParameters,
        Dictionary<string, string> validatedParams
    )
    {
        var value = requestParameters.GetValueOrDefault("price", null);
        if (value == null)
        {
            throw new MissingRequiredFieldException(
                new Field
                {
                    Type = "price",
                    Required = true,
                    Label = "price",
                    Name = "price"
                }
            );
        }

        var decimalValue = Convert.ToDecimal(value, CultureInfo.CurrentCulture);

        if (IsPriceInvalid(decimalValue))
        {
            throw new WrongFieldValueException(
                new Field
                {
                    Type = "select",
                    Required = true,
                    Label = "price",
                    Name = "price"
                },
                value
            );
        }

        validatedParams.Add("price", value);
    }

    private bool IsPriceInvalid(decimal decimalValue)
    {
        var donation = _bitPayProperties.Design.Donation;

        return decimalValue > _maxPrice
            || (!donation.EnableOther && !donation.Denominations.Contains(decimalValue));
    }

    private void AddValidatedSelect(
        Dictionary<string, string?> requestParameters,
        string parameterName,
        Dictionary<string, string> validatedParams,
        List<String> allowedValues
    )
    {
        var value = requestParameters.GetValueOrDefault(parameterName, null);
        if (value == null)
        {
            throw new MissingRequiredFieldException(
                new Field
                {
                    Type = "select",
                    Required = true,
                    Label = parameterName,
                    Name = parameterName
                }
            );
        }

        if (!allowedValues.Contains(value))
        {
            throw new WrongFieldValueException(
                new Field
                {
                    Type = "select",
                    Required = true,
                    Label = parameterName,
                    Name = parameterName
                },
                value
            );
        }

        validatedParams.Add(parameterName, value);
    }

    private void AddValidatedText(
        Dictionary<string, string?> requestParameters,
        string parameterName,
        Dictionary<string, string> validatedParams,
        IValidator? validator = null
    )
    {
        var value = requestParameters.GetValueOrDefault(parameterName, null);
        if (value == null)
        {
            throw new MissingRequiredFieldException(
                new Field
                {
                    Type = "text",
                    Required = true,
                    Label = parameterName,
                    Name = parameterName
                }
            );
        }

        if (validator != null && !validator.Execute(value))
        {
            throw new WrongFieldValueException(
                new Field
                {
                    Type = "text",
                    Required = true,
                    Label = parameterName,
                    Name = parameterName
                },
                value
            );
        }

        validatedParams.Add(parameterName, value);
    }
}
