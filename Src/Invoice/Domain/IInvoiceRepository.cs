namespace CsharpKioskDemoDotnet.Invoice.Domain;

public interface IInvoiceRepository
{
    void Save(Invoice invoice);
}