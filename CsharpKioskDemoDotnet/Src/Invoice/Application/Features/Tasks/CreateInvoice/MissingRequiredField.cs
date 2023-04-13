using CsharpKioskDemoDotnet.Shared.BitPayProperties;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class MissingRequiredField : Exception
{
    public MissingRequiredField(Field field)
    {
        Field = field;
    }

    public Field Field { get; init; }
    
    public string GetMessage() {
        return $"Value for field {Field.Label} is missing.";
    }
}