/*
 * Copyright 2023 BitPay.
 * All rights reserved.
 */

namespace CsharpKioskDemoDotnet.Invoice.Domain.Buyer;

public class InvoiceBuyer
{
    public long Id { get; }
    public string? Name { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool? Notify { get; set; }
    public string? BuyerProvidedEmail { get; set; }
    public InvoiceBuyerProvidedInfo InvoiceBuyerProvidedInfo { get; set; }
    
    public InvoiceBuyer(InvoiceBuyerProvidedInfo invoiceBuyerProvidedInfo)
    {
        InvoiceBuyerProvidedInfo = invoiceBuyerProvidedInfo;
    }
    
    internal InvoiceBuyer()
    {
    }
    
    public void Update(InvoiceBuyer invoiceBuyer)
    {
        Name = invoiceBuyer.Name;
        Address1 = invoiceBuyer.Address1;
        Address2 = invoiceBuyer.Address2;
        City = invoiceBuyer.City;
        Region = invoiceBuyer.Region;
        PostalCode = invoiceBuyer.PostalCode;
        Country = invoiceBuyer.Country;
        Email = invoiceBuyer.Email;
        Phone = invoiceBuyer.Phone;
        Notify = invoiceBuyer.Notify;
        BuyerProvidedEmail = invoiceBuyer.BuyerProvidedEmail;
    }
}