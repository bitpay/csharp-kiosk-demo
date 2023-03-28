using CsharpKioskDemoDotnet.Shared.Domain;

namespace CsharpKioskDemoDotnet.Invoice.Domain;

public interface IInvoiceRepository
{
    void Save(Invoice invoice);

    Page<Invoice> FindAllPaginated(
        EntityPageNumber entityPageNumber,
        EntityPageSize entityPageSize
    );

    Invoice FindById(long invoiceId);
}