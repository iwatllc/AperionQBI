using AperionQB.Application.Interfaces;
using AperionQB.Domain.Entities;
using AperionQB.Domain.Entities.BZBQB;
using Microsoft.EntityFrameworkCore;

namespace AperionQB.Infrastructure.Data;

public partial class BzbDbContext : DbContext, IApplicationDbContext
{
    public BzbDbContext(DbContextOptions<BzbDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChargeCategory> Chargecategories { get; set; }

    public virtual DbSet<QBPaymentTypeMapping> QBPaymentTypeMappings { get; set; }

    public virtual DbSet<ChargeCategoryType> Chargecategorytypes { get; set; }

    public virtual DbSet<QBCustomerMapping> BZBQuickBooksCustomerMappings { get; set; }

    public virtual DbSet<QBPayments> PaymentsToMigrateToIntuit { get; set; }

    public virtual DbSet<QBUpdateTransactions> QBUpdateTransactions { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyCommunication> Companycomms { get; set; }

    public virtual DbSet<CompanyContact> Companycontacts { get; set; }

    public virtual DbSet<Companydocument> Companydocuments { get; set; }

    public virtual DbSet<CompanyJobClass> Companyjobclasses { get; set; }

    public virtual DbSet<CompanyLocation> Companylocs { get; set; }

    public virtual DbSet<Companylochour> Companylochours { get; set; }

    public virtual DbSet<Companynote> Companynotes { get; set; }

    public virtual DbSet<Companysubloc> Companysublocs { get; set; }

    public virtual DbSet<CompanyTaxCode> Companytaxcodes { get; set; }

    public virtual DbSet<CompanyTerms> Companyterms { get; set; }

    public virtual DbSet<Configitem> Configitems { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<ContactAddress> Contactaddresses { get; set; }

    public virtual DbSet<Contactbillingdetail> Contactbillingdetails { get; set; }

    public virtual DbSet<ContactCommunication> Contactcomms { get; set; }

    public virtual DbSet<ContactImageDocument> Contactimagedocuments { get; set; }

    public virtual DbSet<ContactNote> Contactnotes { get; set; }

    public virtual DbSet<ContactTaxCode> Contacttaxcodes { get; set; }

    public virtual DbSet<ContactTerms> Contactterms { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceCustomItem> Invoicecustomitems { get; set; }

    public virtual DbSet<InvoiceIdentifier> Invoiceidentifiers { get; set; }

    public virtual DbSet<InvoiceItemType> Invoiceitemtypes { get; set; }

    public virtual DbSet<InvoiceLineItem> Invoicelineitems { get; set; }

    public virtual DbSet<Invoicelineitemdetail> Invoicelineitemdetails { get; set; }

    public virtual DbSet<InvoiceLog> Invoicelogs { get; set; }

    public virtual DbSet<InvoicePayments> Invoicepayments { get; set; }

    public virtual DbSet<Invoicesummary> Invoicesummaries { get; set; }

    public virtual DbSet<Massinvoicepayment> Massinvoicepayments { get; set; }



    public virtual DbSet<Paymenttype> Paymenttypes { get; set; }



    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TaxCode> Taxcodes { get; set; }

    public virtual DbSet<Term> Terms { get; set; }


    async Task IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync();
       
    }
    public override int SaveChanges()
    {
        base.SaveChanges();
        return 0;
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<ChargeCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("chargecategory")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ChargeCategoryTypeId, "FK_chargecategory_chargecategorytypeid");

            entity.HasIndex(e => e.CompanyId, "FK_chargecategory_companyid");

            entity.HasIndex(e => e.ContactId, "FK_chargecategory_contactid");

            entity.HasIndex(e => new { e.Id, e.CompanyId, e.ContactId }, "id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChargeCategoryTypeId).HasColumnName("chargecategorytypeid");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.DefaultValue).HasColumnName("defaultvalue");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Rate)
                .HasPrecision(10, 2)
                .HasColumnName("rate");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("'1'")
                .HasColumnName("statusid");
            entity.Property(e => e.Summary)
                .HasMaxLength(100)
                .HasColumnName("summary");

            entity.HasOne(d => d.Company).WithMany(p => p.ChargeCategories)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_chargecategory_companyid");

            entity.HasOne(d => d.Contact).WithMany(p => p.ChargeCategories)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK_chargecategory_contactid");
        });

        modelBuilder.Entity<ChargeCategoryType>(entity =>
        {
            entity
                .HasKey(e => e.Id);
            entity
                .ToTable("chargecategorytype")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");
            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("'1'")
                .HasColumnName("statusid");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });


        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("company")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.PrimaryContactId, "FK_company_primarycontactid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Aliases)
                .HasMaxLength(255)
                .HasColumnName("aliases");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Discount)
                .HasPrecision(2, 2)
                .HasColumnName("discount");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PrimaryContactId).HasColumnName("primarycontactid");
            entity.Property(e => e.StatusId).HasColumnName("statusid");
            entity.Property(e => e.Website)
                .HasMaxLength(150)
                .HasColumnName("website");

            entity.HasOne(d => d.PrimaryContact).WithMany(p => p.Companies)
                .HasForeignKey(d => d.PrimaryContactId)
                .HasConstraintName("FK_company_primarycontactid");
        });

        modelBuilder.Entity<CompanyCommunication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("companycomm")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.CommunicationSubTypeId, "FK_companycomm_commsubtypeid");

            entity.HasIndex(e => e.CommunicationTypeId, "FK_companycomm_commtypeid");

            entity.HasIndex(e => e.CompanyId, "FK_companycomm_companyid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommunicationSubTypeId).HasColumnName("commsubtypeid");
            entity.Property(e => e.CommunicationTypeId).HasColumnName("commtypeid");
            entity.Property(e => e.CompanyId).HasColumnName("companyid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.PrimaryCommunication).HasColumnName("primary_comm");
            entity.Property(e => e.StatusId).HasColumnName("statusid");
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .HasColumnName("value");



            entity.HasOne(d => d.Company).WithMany(p => p.CompanyCommunications)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_companycomm_companyid");
        });

        modelBuilder.Entity<CompanyContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("companycontact")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.CompanyId, "IX_COMPANYCONTACT_COMPANY");

            entity.HasIndex(e => e.ContactId, "IX_COMPANYCONTACT_CONTACT");

            entity.HasIndex(e => e.DepartmentId, "IX_COMPANYCONTACT_DEPARTMENT");

            entity.HasIndex(e => e.LocationId, "IX_COMPANYCONTACT_LOCATION");

            entity.HasIndex(e => e.SupervisorId, "IX_COMPANYCONTACT_SUPERVISOR");

            entity.HasIndex(e => new { e.CompanyId, e.ContactId }, "UIX_COMPANYCONTACT_COMPANYCONTACT").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("companyid");
            entity.Property(e => e.CompanyJobClassId).HasColumnName("companyjobclassid");
            entity.Property(e => e.ContactId).HasColumnName("contactid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("createdby");
            entity.Property(e => e.DepartmentId).HasColumnName("departmentid");
            entity.Property(e => e.LocationId).HasColumnName("locationid");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Modifiedby).HasColumnName("modifiedby");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("'1'")
                .HasColumnName("statusid");
            entity.Property(e => e.SupervisorId).HasColumnName("supervisorid");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyContacts)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_COMPANYCONTACT_COMPANY");

            entity.HasOne(d => d.Contact).WithMany(p => p.CompanyContacts)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK_COMPANYCONTACT_CONTACT");


            entity.HasOne(d => d.Location).WithMany(p => p.CompanyContacts)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_COMPANYCONTACT_LOCATION");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.Supervisors)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("FK_COMPANYCONTACT_SUPERVISOR");
        });

        modelBuilder.Entity<Companydocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("companydocument")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.CompanyId, "fk_company_documents_company_data_items1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("companyid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("createdby");
            entity.Property(e => e.DocumentDescription)
                .HasMaxLength(200)
                .HasColumnName("document_description");
            entity.Property(e => e.DocumentExtension)
                .HasMaxLength(8)
                .HasColumnName("document_extension");
            entity.Property(e => e.DocumentTitle)
                .HasMaxLength(100)
                .HasColumnName("document_title");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy).HasColumnName("modifiedby");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyDocuments)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_company_documents_company_data_items1");
        });

        modelBuilder.Entity<CompanyJobClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("companyjobclass")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChargeCategoryId).HasColumnName("chargecategoryid");
            entity.Property(e => e.CompanyId).HasColumnName("companyid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.JobClassId).HasColumnName("jobclassid");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Rate)
                .HasPrecision(10, 2)
                .HasColumnName("rate");
            entity.Property(e => e.StatusId).HasColumnName("statusid");
        });

        modelBuilder.Entity<CompanyLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("companyloc")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.CompanyId, "FK_companyloc_companyid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address1)
                .HasMaxLength(50)
                .HasColumnName("address1");
            entity.Property(e => e.Address2)
                .HasMaxLength(50)
                .HasColumnName("address2");
            entity.Property(e => e.Attention)
                .HasMaxLength(50)
                .HasColumnName("attention");
            entity.Property(e => e.Billing).HasColumnName("billing");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.CompanyId).HasColumnName("companyid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.DepartmentId).HasColumnName("departmentid");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PrimaryLocation).HasColumnName("primaryloc");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .HasColumnName("state");
            entity.Property(e => e.StatusId).HasColumnName("statusid");
            entity.Property(e => e.Zip)
                .HasMaxLength(5)
                .HasColumnName("zip");
            entity.Property(e => e.Zipplus)
                .HasMaxLength(4)
                .HasColumnName("zipplus");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyLocations)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_companyloc_companyid");
        });

        modelBuilder.Entity<Companylochour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("companylochours")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClosedFriday).HasColumnName("closedfriday");
            entity.Property(e => e.ClosedMonday).HasColumnName("closedmonday");
            entity.Property(e => e.ClasedSaturday).HasColumnName("closedsaturday");
            entity.Property(e => e.Closedsunday).HasColumnName("closedsunday");
            entity.Property(e => e.ClosedThursday).HasColumnName("closedthursday");
            entity.Property(e => e.ClosedTuesday).HasColumnName("closedtuesday");
            entity.Property(e => e.ClosedWednesday).HasColumnName("closedwednesday");
            entity.Property(e => e.CompanyLocationId).HasColumnName("companylocid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.FridayEnd)
                .HasMaxLength(15)
                .HasColumnName("fridayend");
            entity.Property(e => e.FridayStart)
                .HasMaxLength(15)
                .HasColumnName("fridaystart");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.MondayEnd)
                .HasMaxLength(15)
                .HasColumnName("mondayend");
            entity.Property(e => e.MondayStart)
                .HasMaxLength(15)
                .HasColumnName("mondaystart");
            entity.Property(e => e.SaturdayEnd)
                .HasMaxLength(15)
                .HasColumnName("saturdayend");
            entity.Property(e => e.SaturdayStart)
                .HasMaxLength(15)
                .HasColumnName("saturdaystart");
            entity.Property(e => e.SundayEnd)
                .HasMaxLength(15)
                .HasColumnName("sundayend");
            entity.Property(e => e.SundayStart)
                .HasMaxLength(15)
                .HasColumnName("sundaystart");
            entity.Property(e => e.ThursdayEnd)
                .HasMaxLength(15)
                .HasColumnName("thursdayend");
            entity.Property(e => e.ThursdayStart)
                .HasMaxLength(15)
                .HasColumnName("thursdaystart");
            entity.Property(e => e.TuesdayEnd)
                .HasMaxLength(15)
                .HasColumnName("tuesdayend");
            entity.Property(e => e.TuesdayStart)
                .HasMaxLength(15)
                .HasColumnName("tuesdaystart");
            entity.Property(e => e.WednesdayEnd)
                .HasMaxLength(15)
                .HasColumnName("wednesdayend");
            entity.Property(e => e.WednesdayStart)
                .HasMaxLength(15)
                .HasColumnName("wednesdaystart");
        });

        modelBuilder.Entity<Companynote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("companynotes")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Chargecategoryid, "FK_companynotes_chargecategoryid");

            entity.HasIndex(e => e.Companyid, "FK_companynotes_companyid");

            entity.HasIndex(e => e.Invoicelineid, "FK_companynotes_invoicelineid");

            entity.HasIndex(e => e.Userid, "FK_companynotes_userid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Billable).HasColumnName("billable");
            entity.Property(e => e.Billed).HasColumnName("billed");
            entity.Property(e => e.Chargecategoryid).HasColumnName("chargecategoryid");
            entity.Property(e => e.Companyid).HasColumnName("companyid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Enddate)
                .HasColumnType("datetime")
                .HasColumnName("enddate");
            entity.Property(e => e.Invoicelineid).HasColumnName("invoicelineid");
            entity.Property(e => e.Locked).HasColumnName("locked");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.Startdate)
                .HasColumnType("datetime")
                .HasColumnName("startdate");
            entity.Property(e => e.Timesheet).HasColumnName("timesheet");
            entity.Property(e => e.Timespent).HasColumnName("timespent");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Chargecategory).WithMany(p => p.CompanyNotes)
                .HasForeignKey(d => d.Chargecategoryid)
                .HasConstraintName("FK_companynotes_chargecategoryid");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyNotes)
                .HasForeignKey(d => d.Companyid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_companynotes_companyid");

            entity.HasOne(d => d.Invoiceline).WithMany(p => p.CompanyNotes)
                .HasForeignKey(d => d.Invoicelineid)
                .HasConstraintName("FK_companynotes_invoicelineid");

        });

        modelBuilder.Entity<Companysubloc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("companysubloc")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Companylocid).HasColumnName("companylocid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Subloc)
                .HasMaxLength(255)
                .HasColumnName("subloc")
                .UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");
        });

        modelBuilder.Entity<CompanyTaxCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("companytaxcode")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.CompanyId, "FK_companytaxcode_companyid");

            entity.HasIndex(e => e.Taxcodeid, "FK_companytaxcode_taxcodeid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("companyid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Taxcodeid).HasColumnName("taxcodeid");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyTaxCodes)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_companytaxcode_companyid");

            entity.HasOne(d => d.TaxCode).WithMany(p => p.CompanyTaxCodes)
                .HasForeignKey(d => d.Taxcodeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_companytaxcode_taxcodeid");
        });

        modelBuilder.Entity<CompanyTerms>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("companyterms")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.CompanyId, "FK_companyterms_companyid");

            entity.HasIndex(e => e.TermsId, "FK_companyterms_termsid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("companyid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.TermsId).HasColumnName("termsid");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyTerms)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_companyterms_companyid");

            entity.HasOne(d => d.Terms).WithMany(p => p.CompanyTerms)
                .HasForeignKey(d => d.TermsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_companyterms_termsid");
        });

        modelBuilder.Entity<Configitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("configitem")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Value)
                .HasMaxLength(500)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("contact")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate)
                .HasColumnName("birthdate");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .HasColumnName("gender");
            entity.Property(e => e.Identifier)
                .HasMaxLength(50)
                .HasColumnName("identifier");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("middlename");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .HasColumnName("nickname");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasColumnName("notes");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("'1'")
                .HasColumnName("statusid");
            entity.Property(e => e.Suffix)
                .HasMaxLength(10)
                .HasColumnName("suffix");
        });

        modelBuilder.Entity<ContactAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("contactaddress")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.AddressTypeId, "FK_contactaddress_addresstypeid");

            entity.HasIndex(e => e.ContactId, "FK_contactaddress_contactid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address1)
                .HasMaxLength(50)
                .HasColumnName("address1");
            entity.Property(e => e.Address2)
                .HasMaxLength(50)
                .HasColumnName("address2");
            entity.Property(e => e.AddressTypeId).HasColumnName("addresstypeid");
            entity.Property(e => e.Billing).HasColumnName("billing");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.ContactId).HasColumnName("contactid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.PrimaryAddress).HasColumnName("primary_address");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("'1'")
                .HasColumnName("statusid");
            entity.Property(e => e.Zip)
                .HasMaxLength(5)
                .HasColumnName("zip");
            entity.Property(e => e.Zipplus)
                .HasMaxLength(4)
                .HasColumnName("zipplus");

            entity.HasOne(d => d.Contact).WithMany(p => p.ContactAddress)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_contactaddress_contactid");
        });

        modelBuilder.Entity<Contactbillingdetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("contactbillingdetail");

            entity.Property(e => e.Bdate)
                .HasColumnType("datetime")
                .HasColumnName("bdate");
            entity.Property(e => e.Bevent)
                .HasMaxLength(16)
                .HasDefaultValueSql("''")
                .HasColumnName("bevent")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ContactId).HasColumnName("contactid");
            entity.Property(e => e.ContactName)
                .HasMaxLength(101)
                .HasColumnName("contactname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.EventAbbr)
                .HasMaxLength(6)
                .HasDefaultValueSql("''")
                .HasColumnName("eventabbr")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ItemIdentifier)
                .HasMaxLength(277)
                .HasDefaultValueSql("''")
                .HasColumnName("itemidentifier")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Notes)
                .HasColumnType("mediumtext")
                .HasColumnName("notes")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Rate)
                .HasPrecision(10, 2)
                .HasColumnName("rate");
            entity.Property(e => e.Summary)
                .HasMaxLength(100)
                .HasColumnName("summary")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Technician)
                .HasMaxLength(101)
                .HasColumnName("technician")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Timespent).HasColumnName("timespent");
        });

        modelBuilder.Entity<ContactCommunication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("contactcomm")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.CommunicationSubTypeId, "FK_contactcomm_commsubtypeid");

            entity.HasIndex(e => e.CommunicationTypeId, "FK_contactcomm_commtypeid");

            entity.HasIndex(e => e.ContactId, "FK_contactcomm_contactid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommunicationSubTypeId).HasColumnName("commsubtypeid");
            entity.Property(e => e.CommunicationTypeId).HasColumnName("commtypeid");
            entity.Property(e => e.ContactId).HasColumnName("contactid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.GoogleCommunication).HasColumnName("google_comm");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.PrimaryCommunication).HasColumnName("primary_comm");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("'1'")
                .HasColumnName("statusid");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .HasColumnName("value");



            entity.HasOne(d => d.Contact).WithMany(p => p.ContactCommunications)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_contactcomm_contactid");
        });

        modelBuilder.Entity<ContactImageDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("contactimagedocument")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ContactImageId, "fk_contact_image_documents_contact_image_data_items1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactImageId).HasColumnName("contactimageid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("createdby");
            entity.Property(e => e.DocumentDescription)
                .HasMaxLength(200)
                .HasColumnName("document_description");
            entity.Property(e => e.DocumentExtension)
                .HasMaxLength(8)
                .HasColumnName("document_extension");
            entity.Property(e => e.DocumentTitle)
                .HasMaxLength(100)
                .HasColumnName("document_title");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy).HasColumnName("modifiedby");

            entity.HasOne(d => d.ContactImage).WithMany(p => p.ContactImageDocuments)
                .HasForeignKey(d => d.ContactImageId)
                .HasConstraintName("fk_contact_image_documents_contact_image_data_items1");
        });

        modelBuilder.Entity<ContactNote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("contactnotes")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ChargeCategoryId, "FK_contactnotes_chargecategoryid");

            entity.HasIndex(e => e.ContactId, "FK_contactnotes_contactid");

            entity.HasIndex(e => e.InvoiceLineId, "FK_contactnotes_invoicelineid");

            entity.HasIndex(e => e.UserId, "FK_contactnotes_userid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Billable).HasColumnName("billable");
            entity.Property(e => e.Billed).HasColumnName("billed");
            entity.Property(e => e.ChargeCategoryId).HasColumnName("chargecategoryid");
            entity.Property(e => e.ContactId).HasColumnName("contactid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Enddate)
                .HasColumnType("datetime")
                .HasColumnName("enddate");
            entity.Property(e => e.InvoiceLineId).HasColumnName("invoicelineid");
            entity.Property(e => e.Locked).HasColumnName("locked");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startdate");
            entity.Property(e => e.Timesheet).HasColumnName("timesheet");
            entity.Property(e => e.Timespent).HasColumnName("timespent");
            entity.Property(e => e.UserId).HasColumnName("userid");

            entity.HasOne(d => d.ChargeCategory).WithMany(p => p.ContactNotes)
                .HasForeignKey(d => d.ChargeCategoryId)
                .HasConstraintName("FK_contactnotes_chargecategoryid");

            entity.HasOne(d => d.Contact).WithMany(p => p.ContactNotes)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_contactnotes_contactid");

            entity.HasOne(d => d.InvoiceLine).WithMany(p => p.ContactNotes)
                .HasForeignKey(d => d.InvoiceLineId)
                .HasConstraintName("FK_contactnotes_invoicelineid");

        });

        modelBuilder.Entity<ContactTaxCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("contacttaxcode")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ContactId, "FK_contacttaxcode_contactid");

            entity.HasIndex(e => e.TaxCodeId, "FK_contacttaxcode_taxcodeid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactId).HasColumnName("contactid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.TaxCodeId).HasColumnName("taxcodeid");

            entity.HasOne(d => d.Contact).WithMany(p => p.ContactTaxCodes)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_contacttaxcode_contactid");

            entity.HasOne(d => d.TaxCode).WithMany(p => p.ContactTaxCodes)
                .HasForeignKey(d => d.TaxCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_contacttaxcode_taxcodeid");
        });

        modelBuilder.Entity<ContactTerms>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("contactterms")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ContactId, "FK_contactterms_contactid");

            entity.HasIndex(e => e.TermsId, "FK_contactterms_termsid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactId).HasColumnName("contactid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.TermsId).HasColumnName("termsid");

            entity.HasOne(d => d.Contact).WithMany(p => p.ContactTerms)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_contactterms_contactid");

            entity.HasOne(d => d.Terms).WithMany(p => p.ContactTerms)
                .HasForeignKey(d => d.TermsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_contactterms_termsid");
        });



        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("invoice")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.TermId, "FK_Invoice_Term");

            entity.HasIndex(e => e.CompanyId, "companyid");

            entity.HasIndex(e => e.ContactId, "contactid");

            entity.HasIndex(e => e.PoNumber, "ponumber");

            entity.HasIndex(e => e.TaxCodeId, "taxcode");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompanyId).HasColumnName("COMPANYID");
            entity.Property(e => e.ContactId).HasColumnName("CONTACTID");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("CREATED");
            entity.Property(e => e.CreatedBy).HasColumnName("CREATEDBY");
            entity.Property(e => e.Due).HasColumnName("DUE");
            entity.Property(e => e.Identifier)
                .HasMaxLength(255)
                .HasColumnName("IDENTIFIER");
            entity.Property(e => e.InvoiceDate).HasColumnName("invoice_date");
            entity.Property(e => e.Memo)
                .HasColumnType("text")
                .HasColumnName("memo");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.PaidInFull).HasColumnName("paid_in_full");
            entity.Property(e => e.PoNumber)
                .HasMaxLength(50)
                .HasColumnName("PONUMBER");
            entity.Property(e => e.TaxCodeId).HasColumnName("TAXCODEID");
            entity.Property(e => e.TermId).HasColumnName("TERMID");
            entity.Property(e => e.TotalCharges).HasColumnName("total_charges")
                .HasPrecision(10, 2);
            entity.Property(e => e.TotalPayments).HasColumnName("total_payments")
                .HasPrecision(10, 2);
            entity.Property(e => e.Void).HasColumnName("void");

            entity.HasOne(d => d.Company).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Invoice_Company");

            entity.HasOne(d => d.Contact).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("FK_Invoice_Contact");

            entity.HasOne(d => d.Taxcode).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.TaxCodeId)
                .HasConstraintName("FK_Invoice_Taxcode");

            entity.HasOne(d => d.Term).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.TermId)
                .HasConstraintName("FK_Invoice_Term");

            entity.Navigation(e => e.Contact).AutoInclude();
            entity.Navigation(e => e.Company).AutoInclude();
        });

        modelBuilder.Entity<InvoiceCustomItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("invoicecustomitem")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.ChargeCategoryId, "FK_invoicecustomitem_chargecategoryid");

            entity.HasIndex(e => e.InvoiceLineId, "FK_invoicecustomitem_invoicelineid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChargeCategoryId).HasColumnName("chargecategoryid");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("createdby");
            entity.Property(e => e.CustomCCName)
                .HasMaxLength(255)
                .HasColumnName("customccname");
            entity.Property(e => e.Details)
                .HasColumnType("mediumtext")
                .HasColumnName("details");
            entity.Property(e => e.InvoiceLineId).HasColumnName("invoicelineid");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.ModifiedBy).HasColumnName("modifiedby");
            entity.Property(e => e.Qty)
                .HasPrecision(10, 2)
                .HasColumnName("qty");
            entity.Property(e => e.Rate)
                .HasPrecision(10, 2)
                .HasColumnName("rate");

            entity.HasOne(d => d.ChargeCategory).WithMany(p => p.InvoiceCustomItems)
                .HasForeignKey(d => d.ChargeCategoryId)
                .HasConstraintName("FK_invoicecustomitem_chargecategoryid");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceCustomItems)
                .HasForeignKey(d => d.InvoiceLineId)
                .HasConstraintName("FK_invoicecustomitem_invoicelineid");
        });

        modelBuilder.Entity<InvoiceIdentifier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("invoiceidentifier")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Identifier)
                .HasMaxLength(100)
                .HasColumnName("identifier");
            entity.Property(e => e.InvoiceId).HasColumnName("invoiceid");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
        });

        modelBuilder.Entity<InvoiceItemType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("invoiceitemtype")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Name)
                .HasColumnType("tinytext")
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
        });

        modelBuilder.Entity<InvoiceLineItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("invoicelineitems")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.CompanyNoteId, "FK_Invoicelineitems_companynoteid");

            entity.HasIndex(e => e.ContactNoteId, "FK_Invoicelineitems_contactnoteid");

            entity.HasIndex(e => e.CustomInvoiceId, "FK_Invoicelineitems_custominvoiceid");

            entity.HasIndex(e => e.ProjectTaskActionId, "FK_Invoicelineitems_projecttaskactionid");

            entity.HasIndex(e => e.TicketActionId, "FK_Invoicelineitems_ticketactionid");

            entity.HasIndex(e => e.TicketChargeId, "FK_Invoicelineitems_ticketcharge");

            entity.HasIndex(e => e.TicketPartId, "FK_Invoicelineitems_ticketpartid");

            entity.HasIndex(e => e.ToDoActionId, "FK_Invoicelineitems_todoactionid");

            entity.HasIndex(e => e.InvoiceId, "invoiceid");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnName("AMOUNT")
                .HasPrecision(10, 2);
            entity.Property(e => e.CompanyNoteId).HasColumnName("COMPANYNOTEID");
            entity.Property(e => e.ContactNoteId).HasColumnName("CONTACTNOTEID");
            entity.Property(e => e.CustomInvoiceId).HasColumnName("CUSTOMINVOICEID");
            entity.Property(e => e.Ignored).HasColumnName("IGNORED");
            entity.Property(e => e.InvoiceId).HasColumnName("INVOICEID");
            entity.Property(e => e.ProjectTaskActionId).HasColumnName("PROJECTTASKACTIONID");
            entity.Property(e => e.Taxable).HasColumnName("TAXABLE");
            entity.Property(e => e.TicketActionId).HasColumnName("TICKETACTIONID");
            entity.Property(e => e.TicketChargeId).HasColumnName("TICKETCHARGEID");
            entity.Property(e => e.TicketPartId).HasColumnName("TICKETPARTID");
            entity.Property(e => e.ToDoActionId).HasColumnName("TODOACTIONID");

            entity.HasOne(d => d.CompanyNote).WithMany(p => p.Invoicelineitems)
                .HasForeignKey(d => d.CompanyNoteId)
                .HasConstraintName("FK_Invoicelineitems_companynoteid");

            entity.HasOne(d => d.ContactNote).WithMany(p => p.Invoicelineitems)
                .HasForeignKey(d => d.ContactNoteId)
                .HasConstraintName("FK_Invoicelineitems_contactnoteid");

            entity.HasOne(d => d.CustomInvoice).WithMany(p => p.InvoiceLineItems)
                .HasForeignKey(d => d.CustomInvoiceId)
                .HasConstraintName("FK_Invoicelineitems_custominvoiceid");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLineItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_invoicelineitems_invoiceid");

            //entity.HasOne(d => d.ProjectTaskAction).WithMany(p => p.Invoicelineitems)
            //    .HasForeignKey(d => d.ProjectTaskActionId)
            //    .HasConstraintName("FK_Invoicelineitems_projecttaskactionid");

            //entity.HasOne(d => d.TicketAction).WithMany(p => p.Invoicelineitems)
            //    .HasForeignKey(d => d.TicketActionId)
            //    .HasConstraintName("FK_Invoicelineitems_ticketactionid");

            //entity.HasOne(d => d.TicketCharge).WithMany(p => p.Invoicelineitems)
            //    .HasForeignKey(d => d.TicketChargeId)
            //    .HasConstraintName("FK_Invoicelineitems_ticketcharge");

            //entity.HasOne(d => d.TicketPart).WithMany(p => p.Invoicelineitems)
            //    .HasForeignKey(d => d.TicketPartId)
            //    .HasConstraintName("FK_Invoicelineitems_ticketpartid");

            //entity.HasOne(d => d.ToDoAction).WithMany(p => p.Invoicelineitems)
            //    .HasForeignKey(d => d.ToDoActionId)
            //    .HasConstraintName("FK_Invoicelineitems_todoactionid");
        });

        modelBuilder.Entity<Invoicelineitemdetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("invoicelineitemdetail");

            entity.Property(e => e.Actiondate)
                .HasColumnType("datetime")
                .HasColumnName("actiondate");
            entity.Property(e => e.Actionid).HasColumnName("actionid");
            entity.Property(e => e.Actionnotes)
                .HasColumnName("actionnotes")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Bevent)
                .HasColumnType("text")
                .HasColumnName("bevent")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Chargecategoryid)
                .HasMaxLength(11)
                .HasColumnName("chargecategoryid")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Companynoteid).HasColumnName("companynoteid");
            entity.Property(e => e.Contactnoteid).HasColumnName("contactnoteid");
            entity.Property(e => e.Customccname)
                .HasMaxLength(255)
                .HasColumnName("customccname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Customdetails)
                .HasColumnName("customdetails")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Custominvoiceid).HasColumnName("custominvoiceid");
            entity.Property(e => e.Custominvoicetypeid)
                .HasMaxLength(11)
                .HasColumnName("custominvoicetypeid")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Customqty)
                .HasMaxLength(12)
                .HasDefaultValueSql("''")
                .HasColumnName("customqty")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3")
                .HasPrecision(10, 2);
            entity.Property(e => e.Customrate)
                .HasMaxLength(12)
                .HasDefaultValueSql("''")
                .HasColumnName("customrate")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Eventabbr)
                .HasMaxLength(9)
                .HasDefaultValueSql("''")
                .HasColumnName("eventabbr")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Identifier)
                .HasColumnType("mediumtext")
                .HasColumnName("identifier")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Ignored).HasColumnName("ignored");
            entity.Property(e => e.Invoiceid).HasColumnName("invoiceid");
            entity.Property(e => e.Projecttaskactionid).HasColumnName("projecttaskactionid");
            entity.Property(e => e.Rate)
                .HasPrecision(10, 2)
                .HasColumnName("rate");
            entity.Property(e => e.Summary)
                .HasMaxLength(100)
                .HasColumnName("summary")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Taxable).HasColumnName("taxable");
            entity.Property(e => e.Technician)
                .HasMaxLength(101)
                .HasColumnName("technician")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Ticketactionid).HasColumnName("ticketactionid");
            entity.Property(e => e.Ticketchargeid).HasColumnName("ticketchargeid");
            entity.Property(e => e.Ticketpartid).HasColumnName("ticketpartid");
            entity.Property(e => e.Timespent).HasColumnName("timespent");
            entity.Property(e => e.Todoactionid).HasColumnName("todoactionid");
        });

        modelBuilder.Entity<InvoiceLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("invoicelog")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateSent)
                .HasColumnType("datetime")
                .HasColumnName("datesent");
            entity.Property(e => e.EmailCC)
                .HasColumnType("text")
                .HasColumnName("emailcc");
            entity.Property(e => e.EmailFrom)
                .HasColumnType("text")
                .HasColumnName("emailfrom");
            entity.Property(e => e.EmailMessage)
                .HasColumnType("text")
                .HasColumnName("emailmessage");
            entity.Property(e => e.EmailSubject)
                .HasColumnType("text")
                .HasColumnName("emailsubject");
            entity.Property(e => e.EmailTo)
                .HasColumnType("text")
                .HasColumnName("emailto");
            entity.Property(e => e.InvoiceIds)
                .HasColumnType("text")
                .HasColumnName("invoiceids");
        });

        modelBuilder.Entity<InvoicePayments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("invoicepayments")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.InvoiceId, "FK_invoicepayments_invoiceid");

            entity.HasIndex(e => e.MassInvoicePaymentId, "FK_invoicepayments_massinvoicepaymentid");

            entity.HasIndex(e => e.PaymentTypeId, "FK_invoicepayments_paymenttypeid");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("AMOUNT");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Createdby)
                .HasMaxLength(25)
                .HasColumnName("createdby");
            entity.Property(e => e.DatePaid).HasColumnName("DATEPAID");
            entity.Property(e => e.InvoiceId).HasColumnName("INVOICEID");
            entity.Property(e => e.MassInvoicePaymentId).HasColumnName("MASSINVOICEPAYMENTID");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp")
                .HasColumnName("modified");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(25)
                .HasColumnName("modifiedby");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("NOTES");
            entity.Property(e => e.PaymentNumber)
                .HasMaxLength(25)
                .HasColumnName("payment_number");
            entity.Property(e => e.PaymentTypeId).HasColumnName("PAYMENTTYPEID");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoicePayments)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_invoicepayments_invoiceid");

            entity.HasOne(d => d.MassInvoicePayment).WithMany(p => p.Invoicepayments)
                .HasForeignKey(d => d.MassInvoicePaymentId)
                .HasConstraintName("FK_invoicepayments_massinvoicepaymentid");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Invoicepayments)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_invoicepayments_paymenttypeid");
        });

        modelBuilder.Entity<Invoicesummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("invoicesummary");

            entity.Property(e => e.Charges)
                .HasPrecision(10, 2)
                .HasColumnName("charges");
            entity.Property(e => e.Companyid).HasColumnName("companyid");
            entity.Property(e => e.Companyname)
                .HasMaxLength(50)
                .HasColumnName("companyname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Contactid).HasColumnName("contactid");
            entity.Property(e => e.Contactname)
                .HasMaxLength(101)
                .HasColumnName("contactname")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Due).HasColumnName("due");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Identifier)
                .HasMaxLength(255)
                .HasColumnName("identifier")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.InvoiceDate).HasColumnName("invoice_date");
            entity.Property(e => e.Nontaxable).HasColumnName("nontaxable");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Numignored)
                .HasPrecision(23)
                .HasColumnName("numignored");
            entity.Property(e => e.Numlines).HasColumnName("numlines");
            entity.Property(e => e.Payments)
                .HasPrecision(10, 2)
                .HasColumnName("payments");
            entity.Property(e => e.Ponumber)
                .HasMaxLength(50)
                .HasColumnName("ponumber")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Taxable).HasColumnName("taxable");
            entity.Property(e => e.Taxes)
                .HasPrecision(9, 2)
                .HasColumnName("taxes");
            entity.Property(e => e.Void).HasColumnName("void");
        });

        modelBuilder.Entity<Massinvoicepayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("massinvoicepayment")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.Paymenttypeid, "FK_massinvoicepayment_paymenttypeid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Createdby)
                .HasMaxLength(25)
                .HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Datepaid)
                .HasColumnType("datetime")
                .HasColumnName("datepaid");
            entity.Property(e => e.Invoicepaymentids)
                .HasColumnType("text")
                .HasColumnName("invoicepaymentids");
            entity.Property(e => e.Modified)
                .HasColumnType("timestamp")
                .HasColumnName("modified");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(25)
                .HasColumnName("modifiedby");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.PaymentNumber)
                .HasMaxLength(25)
                .HasColumnName("payment_number");
            entity.Property(e => e.Paymenttypeid).HasColumnName("paymenttypeid");

            entity.HasOne(d => d.Paymenttype).WithMany(p => p.Massinvoicepayments)
                .HasForeignKey(d => d.Paymenttypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_massinvoicepayment_paymenttypeid");
        });



        modelBuilder.Entity<Paymenttype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("paymenttype")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Defaultpt).HasColumnName("defaultpt");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("status")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Statusid)
                .HasDefaultValueSql("'1'")
                .HasColumnName("statusid");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .HasColumnName("value");
        });

        modelBuilder.Entity<TaxCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("taxcode")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Defaulttc).HasColumnName("defaulttc");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Rate).HasColumnName("rate").HasPrecision(10, 2);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Term>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("terms")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.Defaultt).HasColumnName("defaultt");
            entity.Property(e => e.Modified)
                .HasColumnType("datetime")
                .HasColumnName("modified");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
        });

        modelBuilder.Entity<QBCustomerMapping>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity
                .ToTable("QBCustomerMapping")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.CompanyID).HasColumnName("CompanyID");
            entity.Property(e => e.qbId).HasColumnName("qbId");

        });

        modelBuilder.Entity<QBPaymentTypeMapping>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity
                .ToTable("QBPaymentTypeMapping")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.bzbPaymentTypeID).HasColumnName("bzbPaymentTypeID");
            entity.Property(e => e.intuitPaymentTypeID).HasColumnName("intuitPaymentTypeID");

        });


        modelBuilder.Entity<QBPayments>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity
                .ToTable("QBPayments")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.InvoiceID).HasColumnName("InvoiceID");
            entity.Property(e => e.InvoicePaymentID).HasColumnName("InvoicePaymentID");
            entity.Property(e => e.BZBCompanyID).HasColumnName("BZBCompanyID");
            entity.Property(e => e.totalAmount).HasColumnName("totalAmount").HasPrecision(10, 2);
            entity.Property(e => e.Memo).HasColumnName("Memo").HasColumnType("Text");
            entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate").HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy").HasColumnType("Text");
            entity.Property(e => e.DeletedBool).HasColumnName("DeletedBool");
            entity.Property(e => e.DeletedBy).HasColumnName("DeletedBy").HasColumnType("Text");
            entity.Property(e => e.DeletedByDate).HasColumnName("DeletedByDate").HasColumnType("datetime");
            entity.Property(e => e.DeletedByQBIDate).HasColumnName("DeletedByQBIDate").HasColumnType("datetime");
            entity.Property(e => e.intuitPaymentID).HasColumnName("intuitPaymentID");
        });

        modelBuilder.Entity<QBUpdateTransactions>(entity =>
        {
            entity.HasKey(e => e.id).HasName("primary");

            entity
                .ToTable("QBUpdateTransactions")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.QBPaymentsID).HasColumnName("QBPaymentsID");
            entity.Property(e => e.updateBool).HasColumnName("updateBoolean");
            entity.Property(e => e.updatedDate).HasColumnName("updatedDate").HasColumnType("datetime");
            entity.Property(e => e.updatedUser).HasColumnName("updatedUser").HasColumnType("Text");
            entity.Property(e => e.datePosted).HasColumnName("datePosted").HasColumnType("datetime");


        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}