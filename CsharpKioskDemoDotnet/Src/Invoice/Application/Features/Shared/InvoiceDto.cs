namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;

public class InvoiceDto
{
    public long Id { get; }
    public string BitPayId { get; }
    public string Price { get; }
    public string CreatedDate { get; }
    public string? BitPayOrderId { get; }
    public string Status { get; }
    public string? Description { get; }

    public InvoiceDto(
        long id,
        string bitPayId,
        string price,
        string createdDate,
        string status,
        string? bitPayOrderId,
        string? description
    )
    {
        Id = id;
        BitPayId = bitPayId;
        Price = price;
        CreatedDate = createdDate;
        BitPayOrderId = bitPayOrderId;
        Status = status;
        Description = description;
    }
}