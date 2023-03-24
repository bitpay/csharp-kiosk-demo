using CsharpKioskDemoDotnet.Shared.BitPayProperties;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class MissingRequiredField : Exception
{
    public Field Field { get; init; }
    
    public string GetMessage() {
        return $"Value for field {Field.Label} is missing.";
    }
}