using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MinimalApi.Models
{
    public partial class CertificateMSContext : DbContext
    {
        public CertificateMSContext()
        {
        }

        public CertificateMSContext(DbContextOptions<CertificateMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApvStatus> ApvStatuses { get; set; } = null!;
        public virtual DbSet<Campus> Campuses { get; set; } = null!;
        public virtual DbSet<CertApplication> CertApplications { get; set; } = null!;
        public virtual DbSet<Convocation> Convocations { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Program> Programs { get; set; } = null!;
        public virtual DbSet<StudentType> StudentTypes { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApvStatus>(entity =>
            {
                entity.ToTable("ApvStatus");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Campus>(entity =>
            {
                entity.ToTable("Campus");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.CampusName)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(' ')");
            });

            modelBuilder.Entity<CertApplication>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApplyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ApprovedByAcad)
                    .HasColumnName("ApprovedByACAD")
                    .HasComment("User Id who approved In ACAD section");

                entity.Property(e => e.ApprovedByAcc).HasComment("User Id who approved In Accounts section");

                entity.Property(e => e.ApprovedByDept).HasComment("User Id who approved In Department section");

                entity.Property(e => e.ApprovedByLib).HasComment("User Id who approved In Library section");

                entity.Property(e => e.ApvAcaddate)
                    .HasColumnType("datetime")
                    .HasColumnName("ApvACADDate");

                entity.Property(e => e.ApvAccDate).HasColumnType("datetime");

                entity.Property(e => e.ApvDeptDate).HasColumnType("datetime");

                entity.Property(e => e.ApvExamDate).HasColumnType("datetime");

                entity.Property(e => e.ApvLibDate).HasColumnType("datetime");

                entity.Property(e => e.ApvStatusAcad)
                    .HasColumnName("ApvStatusACAD")
                    .HasDefaultValueSql("((1))")
                    .HasComment("Approval StatusID For ACADsection");

                entity.Property(e => e.ApvStatusAcc)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Approval StatusID For Accounts section");

                entity.Property(e => e.ApvStatusDept)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Approval Status ID For Dept");

                entity.Property(e => e.ApvStatusExam).HasDefaultValueSql("((1))");

                entity.Property(e => e.ApvStatusLib)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Approval StatusID For library section");

                entity.Property(e => e.ConvocationId).HasColumnName("ConvocationID");

                entity.Property(e => e.ExtraThree).HasDefaultValueSql("(' ')");

                entity.Property(e => e.ExtraTwo).HasDefaultValueSql("(' ')");

                entity.Property(e => e.IsDelivered).HasDefaultValueSql("((0))");

                entity.Property(e => e.PhoneNo).HasMaxLength(50);

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(50)
                    .HasColumnName("StudentID");

                entity.Property(e => e.StudentName).HasMaxLength(50);

                entity.Property(e => e.TrackId).HasColumnName("TrackID");
            });

            modelBuilder.Entity<Convocation>(entity =>
            {
                entity.ToTable("Convocation");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Year)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DeptName).HasMaxLength(50);

                entity.Property(e => e.DeptSname)
                    .HasMaxLength(20)
                    .HasColumnName("DeptSName");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("Program");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProgramName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StudentType>(entity =>
            {
                entity.ToTable("StudentType");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(' ')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
