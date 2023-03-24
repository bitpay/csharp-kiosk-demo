using CsharpKioskDemoDotnet.Invoice.Domain;

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
}