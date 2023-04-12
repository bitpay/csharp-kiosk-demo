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

    Invoice FindByUuid(string invoiceUuid);

    void Update(Invoice invoice);

    List<Invoice> FindAll();
}