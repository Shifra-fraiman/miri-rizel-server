using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CopyRight.Dal.Models;

public partial class CopyrightdbUe70Context : DbContext
{
    public CopyrightdbUe70Context()
    {
    }

    public CopyrightdbUe70Context(DbContextOptions<CopyrightdbUe70Context> options)
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=dpg-cqlndt08fa8c73b73c3g-a.singapore-postgres.render.com;Port=5432;Database=copyrightdb_ue70;Username=miri_rizel;Password=M1HONrwZnY85hnyBMraOFUomYoWzBDNz");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Communication>(entity =>
        {
            entity.HasKey(e => e.CommunicationId).HasName("Communications_pkey");

            entity.Property(e => e.CommunicationId).HasColumnName("communication_id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.RelatedId).HasColumnName("related_id");
            entity.Property(e => e.RelatedTo).HasColumnName("related_to");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");

            entity.HasOne(d => d.RelatedToNavigation).WithMany(p => p.Communications)
                .HasForeignKey(d => d.RelatedTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Communications_related_to_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("Customers_pkey");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.BusinessName)
                .HasMaxLength(100)
                .HasColumnName("business_name");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .HasColumnName("source");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("Customers_status_fkey");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("Documents_pkey");

            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.RelatedId).HasColumnName("related_id");
            entity.Property(e => e.RelatedTo)
                .HasMaxLength(20)
                .HasColumnName("related_to");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Lead>(entity =>
        {
            entity.HasKey(e => e.LeadId).HasName("Leads_pkey");

            entity.Property(e => e.LeadId).HasColumnName("lead_id");
            entity.Property(e => e.BusinessName)
                .HasMaxLength(100)
                .HasColumnName("business_name");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastContactedDate).HasColumnName("last_contacted_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .HasColumnName("source");
        });

        modelBuilder.Entity<PriorityCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PriorityCode_pkey");

            entity.ToTable("PriorityCode");

            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("Projects_pkey");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Authorize).HasColumnName("authorize");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.AuthorizeNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Authorize)
                .HasConstraintName("fk_project_rolecode");

            entity.HasOne(d => d.Customer).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("Projects_customer_id_fkey");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("Projects_status_fkey");
        });

        modelBuilder.Entity<RelatedToCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("relatedToCode_pkey");

            entity.ToTable("relatedToCode");

            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<RoleCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RoleCode_pkey");

            entity.ToTable("RoleCode");

            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<StatusCodeProject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StatusCodeProject_pkey");

            entity.ToTable("StatusCodeProject");

            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<StatusCodeUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StatusCodeUser_pkey");

            entity.ToTable("StatusCodeUser");

            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("Tasks_pkey");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.GoogleId)
                .HasMaxLength(100)
                .HasColumnName("google_id");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("Tasks_assigned_to_fkey");

            entity.HasOne(d => d.PriorityNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Priority)
                .HasConstraintName("Tasks_priority_fkey");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("Tasks_project_id_fkey");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tasks_status_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Users_pkey");

            entity.HasIndex(e => e.Email, "Users_email_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_role_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
