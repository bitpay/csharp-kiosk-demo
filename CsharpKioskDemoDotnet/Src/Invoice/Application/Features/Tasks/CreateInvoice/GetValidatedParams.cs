using CsharpKioskDemoDotnet.Shared.BitPayProperties;
using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class GetValidatedParams
{
    private readonly BitPayProperties _bitPayProperties;

    public GetValidatedParams(IOptions<BitPayProperties> bitPayPropertiesOption) 
    {
        _bitPayProperties = bitPayPropertiesOption.Value;
    }
    
    internal Dictionary<string, string> Execute(Dictionary<string, string?> requestParameters) {
        var validatedParams = new Dictionary<string, string>();

        foreach (var field in _bitPayProperties.GetFields())
        {
            var value = requestParameters.GetValueOrDefault(field.Name, null);
            if (IsMissingRequiredField(field, value)) {
                throw new MissingRequiredField {Field = field};
            }

            if (value != null) {
                validatedParams.Add(field.Name, value);
            }
        }

        return validatedParams; 
    }
    
    private bool IsMissingRequiredField(
        Field field, 
        object? value
    ) { 
        return field.Required && value == null; 
    }
}