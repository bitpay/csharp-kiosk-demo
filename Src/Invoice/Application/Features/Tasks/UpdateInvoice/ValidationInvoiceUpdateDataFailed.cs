namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;

public class ValidationInvoiceUpdateDataFailed : Exception
{
    public Dictionary<string, string> Errors { get; }

    public ValidationInvoiceUpdateDataFailed(Dictionary<string, string> errors)
    {
        Errors = errors;
    }
}