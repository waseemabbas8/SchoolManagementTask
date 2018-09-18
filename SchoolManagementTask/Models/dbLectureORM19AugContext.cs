using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SchoolManagementTask.Models
{
    public partial class dbLectureORM19AugContext : DbContext
    {
        public dbLectureORM19AugContext()
        {
        }

        public dbLectureORM19AugContext(DbContextOptions<dbLectureORM19AugContext> options)
            : base(options)
        {

        }
        
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=WASEEM_THETA\\SQLEXPRESS;Database=dbLectureORM19Aug;Trusted_Connection=True; User ID=sa; Password=6221;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Class)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.RollNo);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Cnic)
                    .HasColumnName("CNIC")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Subject)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
