using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Shared.Domain;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Domain;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly MvcInvoiceContext _context;

    public InvoiceRepository(MvcInvoiceContext context)
    {
        _context = context;
    }

    public void Save(Invoice.Domain.Invoice invoice)
    {
        _context.Add(invoice);
        _context.SaveChanges();
    }

    public Page<Invoice.Domain.Invoice> findAllPaginated(
        EntityPageNumber entityPageNumber,
        EntityPageSize entityPageSize
    )
    {
        var invoices = _context.Invoices;
        var totalElements = invoices.Count();

        var content = invoices
            .OrderBy(b => b.Id)
            .Skip((entityPageNumber.Value - 1) * entityPageSize.Value)
            .Take(entityPageSize.Value)
            .ToList();

        return new Page<Invoice.Domain.Invoice>(
            content: content,
            currentPageNumber: Math.Max(entityPageNumber.Value - 1, 0),
            maxElementsPerPage: entityPageSize.Value,
            totalElements: totalElements,
            totalPages: (int) Math.Ceiling((decimal)totalElements / entityPageSize.Value)
        );
    }
}