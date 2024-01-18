using AperionQB.Domain.Entities;
using AperionQB.Domain.Entities.BZBQB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AperionQB.Application.Interfaces;

public interface IApplicationDbContext
{
    
    public DbSet<ChargeCategory> Chargecategories { get; }
    public DbSet<QBPaymentTypeMapping> QBPaymentTypeMappings { get; }
    public DbSet<QBMassInvoicePayment> QBMassInvoicePayments { get; }
    public DbSet<ChargeCategoryType> Chargecategorytypes { get; }
    public DbSet<QBCustomerMapping> BZBQuickBooksCustomerMappings { get; }
    public DbSet<QBUpdateTransactions> QBUpdateTransactions { get; }
    public DbSet<QBPayments> PaymentsToMigrateToIntuit { get; }
    public DbSet<Company> Companies { get; }
    public DbSet<CompanyCommunication> Companycomms { get; }
    public DbSet<CompanyContact> Companycontacts { get; }
    public DbSet<Companydocument> Companydocuments { get; }
    public DbSet<CompanyJobClass> Companyjobclasses { get; }
    public DbSet<Companylochour> Companylochours { get; }
    public DbSet<CompanyLocation> Companylocs { get; }
    public DbSet<Companynote> Companynotes { get; }
    public DbSet<Companysubloc> Companysublocs { get; }
    public DbSet<CompanyTaxCode> Companytaxcodes { get; }
    public DbSet<CompanyTerms> Companyterms { get; }
    public DbSet<Configitem> Configitems { get; }
    public DbSet<ContactAddress> Contactaddresses { get; }
    public DbSet<Contactbillingdetail> Contactbillingdetails { get; }
    public DbSet<ContactCommunication> Contactcomms { get; }
    public DbSet<ContactImageDocument> Contactimagedocuments { get; }
    public DbSet<ContactNote> Contactnotes { get; }
    public DbSet<Contact> Contacts { get; }
    public DbSet<ContactTaxCode> Contacttaxcodes { get; }
    public DbSet<ContactTerms> Contactterms { get; }
    public DbSet<InvoiceCustomItem> Invoicecustomitems { get; }
    public DbSet<InvoiceIdentifier> Invoiceidentifiers { get; }
    public DbSet<InvoiceItemType> Invoiceitemtypes { get; }
    public DbSet<Invoicelineitemdetail> Invoicelineitemdetails { get; }
    public DbSet<InvoiceLineItem> Invoicelineitems { get; }
    public DbSet<InvoiceLog> Invoicelogs { get; }
    public DbSet<InvoicePayments> Invoicepayments { get; }
    public DbSet<Invoice> Invoices { get; }
    public DbSet<Invoicesummary> Invoicesummaries { get; }
    public DbSet<Massinvoicepayment> Massinvoicepayments { get; }
    public DbSet<Paymenttype> Paymenttypes { get; }
    public DbSet<Status> Statuses { get; }
    public DbSet<TaxCode> Taxcodes { get; }
    public DbSet<Term> Terms { get; }
    
    public Task SaveChangesAsync(CancellationToken cancellationToken = new());
    public int SaveChanges();

}
