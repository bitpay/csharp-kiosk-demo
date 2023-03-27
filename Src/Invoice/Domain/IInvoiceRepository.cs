using CsharpKioskDemoDotnet.Shared.Domain;

namespace CsharpKioskDemoDotnet.Invoice.Domain;

public interface IInvoiceRepository
{
    void Save(Invoice invoice);

    public Page<Invoice> findAllPaginated(
        EntityPageNumber entityPageNumber,
        EntityPageSize entityPageSize
    );
}