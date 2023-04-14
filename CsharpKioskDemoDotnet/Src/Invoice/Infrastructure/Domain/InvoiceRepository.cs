using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Shared.Domain;
using Microsoft.EntityFrameworkCore;

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
    
    public void Update(Invoice.Domain.Invoice invoice)
    {
        _context.Update(invoice);
        _context.SaveChanges();
    }

    public Page<Invoice.Domain.Invoice> FindAllPaginated(
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

    public Invoice.Domain.Invoice FindById(long invoiceId)
    {
        var invoice = _context.Invoices.Find(invoiceId);

        if (invoice == null)
        {
            throw new InvoiceNotFound();
        }

        return invoice;
    }

    public Invoice.Domain.Invoice FindByUuid(string invoiceUuid)
    {
        var invoice = _context.Invoices
            .Include(x => x.InvoicePayment)
            .Include(x => x.InvoiceBuyer)
            .Include(x => x.InvoiceRefund)
            .FirstOrDefault(invoice => invoice.Uuid == invoiceUuid);

        if (invoice == null)
        {
            throw new InvoiceNotFound();
        }

        return invoice;
        
    }

    public List<Invoice.Domain.Invoice> FindAll()
    {
        return _context.Invoices.ToList();
    }
}