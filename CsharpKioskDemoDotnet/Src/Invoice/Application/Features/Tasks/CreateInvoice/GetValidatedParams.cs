// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Shared.BitPayProperties;
using CsharpKioskDemoDotnet.Shared.Form;

using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class GetValidatedParams : IGetValidatedParams
{
    private readonly BitPayProperties _bitPayProperties;

    public GetValidatedParams(IOptions<BitPayProperties> bitPayPropertiesOption)
    {
        ArgumentNullException.ThrowIfNull(bitPayPropertiesOption);
        _bitPayProperties = bitPayPropertiesOption.Value;
    }

    public Dictionary<string, string> Execute(Dictionary<string, string?> requestParameters)
    {
        var validatedParams = new Dictionary<string, string>();

        foreach (var field in _bitPayProperties.Fields)
        {
            var value = requestParameters!.GetValueOrDefault(field.Name, null);
            if (IsMissingRequiredField(field, value))
            {
                throw new MissingRequiredFieldException(field);
            }

            if (value != null)
            {
                validatedParams.Add(field.Name!, value);
            }
        }

        return validatedParams;
    }

    private bool IsMissingRequiredField(
        Field field,
        object? value
    )
    {
        return field.Required && value == null;
    }
}