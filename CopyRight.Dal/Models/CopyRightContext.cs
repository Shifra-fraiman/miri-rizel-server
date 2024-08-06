using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CopyRight.Dal.Models;

public partial class CopyRightContext : DbContext
{
    public CopyRightContext()
    {
    }

    public CopyRightContext(DbContextOptions<CopyRightContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Communication> Communications { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Lead> Leads { get; set; }

    public virtual DbSet<PriorityCode> PriorityCodes { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<RelatedToCode> RelatedToCodes { get; set; }

    public virtual DbSet<RoleCode> RoleCodes { get; set; }

    public virtual DbSet<StatusCodeProject> StatusCodeProjects { get; set; }

    public virtual DbSet<StatusCodeUser> StatusCodeUsers { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //PGPASSWORD=RvXri1TomeRGZIq4MsJVzrmPrNGQvGj0 psql -h dpg-cqkalq2ju9rs738jmrag-a.singapore-postgres.render.com -U copyrightdb_user copyrightdb
        optionsBuilder.UseNpgsql("Host=dpg-cqlndt08fa8c73b73c3g-a.singapore-postgres.render.com;Port=5432;Database=copyrightdb_ue70;Username=miri_rizel;Password=M1HONrwZnY85hnyBMraOFUomYoWzBDNz;");
    }
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source= DESKTOP-E0FAPSB\\SQLEXPRESS;Initial Catalog=CopyRight; Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Communication>(entity =>
        {
            entity.HasKey(e => e.CommunicationId).HasName("PK__Communic__C6D53E4CA417F910");

            entity.Property(e => e.CommunicationId).HasColumnName("communication_id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Details)
                .HasColumnType("text")
                .HasColumnName("details");
            entity.Property(e => e.RelatedId).HasColumnName("related_id");
            entity.Property(e => e.RelatedTo).HasColumnName("related_to");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.RelatedToNavigation).WithMany(p => p.Communications)
                .HasForeignKey(d => d.RelatedTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Communica__relat__49C3F6B7");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB850F8F292D");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.BusinessName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("business_name");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("source");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("FK__Customers__statu__4D94879B");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__9666E8AC686E8FC7");

            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("file_path");
            entity.Property(e => e.RelatedId).HasColumnName("related_id");
            entity.Property(e => e.RelatedTo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("related_to");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => e.LeadId).HasName("PK__Leads__B54D340BF847E999");

            entity.Property(e => e.LeadId).HasColumnName("lead_id");
            entity.Property(e => e.BusinessName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("business_name");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastContactedDate)
                .HasColumnType("datetime")
                .HasColumnName("last_contacted_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("source");
        });

        modelBuilder.Entity<PriorityCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Priority__3214EC071FCD575A");

            entity.ToTable("PriorityCode");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__BC799E1F50222B31");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Customer).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Projects__custom__5165187F");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("FK__Projects__status__52593CB8");
        });

        modelBuilder.Entity<RelatedToCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__relatedT__3214EC07951C0B3C");

            entity.ToTable("relatedToCode");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleCode__3214EC07255A4266");

            entity.ToTable("RoleCode");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusCodeProject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StatusCo__3214EC079DBE9762");

            entity.ToTable("StatusCodeProject");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusCodeUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StatusCo__3214EC07A82ABE7B");

            entity.ToTable("StatusCodeUser");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__0492148DF823C59C");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DueDate)
                .HasColumnType("datetime")
                .HasColumnName("due_date");
            entity.Property(e => e.GoogleId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("google_id");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK__Tasks__assigned___5629CD9C");

            entity.HasOne(d => d.PriorityNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Priority)
                .HasConstraintName("FK__Tasks__priority__5812160E");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Tasks__project_i__571DF1D5");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tasks__status__59063A47");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FF946FB13");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E61645CEA1694").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__role__45F365D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
