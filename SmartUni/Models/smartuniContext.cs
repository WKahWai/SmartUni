using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.Extensions;

namespace SmartUni.Models
{
    public partial class SmartUniContext : DbContext
    {
        public SmartUniContext()
        {
        }

        public SmartUniContext(DbContextOptions<SmartUniContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<ExamSubject> ExamSubject { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentSubject> StudentSubject { get; set; }
        public virtual DbSet<StudyLevel> StudyLevel { get; set; }
        public virtual DbSet<StudyStatus> StudyStatus { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Tutor> Tutor { get; set; }
        public virtual DbSet<TutorStatus> TutorStatus { get; set; }
        public virtual DbSet<TutorType> TutorType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasIndex(e => e.ClassId)
                    .HasName("IX_ClassId");

                entity.Property(e => e.ClassId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ClassDesc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TutorId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.StudyLevel)
                    .WithMany(p => p.Class)
                    .HasForeignKey(d => d.StudyLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudyLevel_Class");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Class)
                    .HasForeignKey(d => d.TutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tutor_Class");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.ExamDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExamTerm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExamSubject>(entity =>
            {
                entity.HasKey(e => new { e.ExamId, e.StudSubjectId });

                entity.HasIndex(e => new { e.ExamId, e.StudSubjectId })
                    .HasName("IX_StudentSubjectId");

                entity.Property(e => e.Grade)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.ExamSubject)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_ExamSubject");

                entity.HasOne(d => d.StudSubject)
                    .WithMany(p => p.ExamSubject)
                    .HasForeignKey(d => d.StudSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentSubject_ExamSubject");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudId)
                    .HasName("PK__Student__F5C0A7FFEEB88BE3");

                entity.HasIndex(e => e.StudId)
                    .HasName("IX_StudId");

                entity.Property(e => e.StudId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ClassId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.StudName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Class_Student");

                entity.HasOne(d => d.StudyStatus)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.StudyStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudyStatus_Student");
            });

            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.HasKey(e => e.StudSubjectId)
                    .HasName("PK__StudentS__9FEECE86AC6942D1");

                entity.HasIndex(e => e.StudSubjectId)
                    .HasName("IX_StudentSubjectId");

                entity.Property(e => e.StudId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentSubject)
                    .HasForeignKey(d => d.StudId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_StudentSubject");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.StudentSubject)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subject_StudentSubject");
            });

            modelBuilder.Entity<StudyLevel>(entity =>
            {
                entity.Property(e => e.StudyLevelDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StudyStatus>(entity =>
            {
                entity.Property(e => e.StudyStatusDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasIndex(e => e.SubjectId)
                    .HasName("IX_SubjectId");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TutorId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Subject)
                    .HasForeignKey(d => d.TutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tutor_Subject");
            });

            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.HasIndex(e => e.TutorId)
                    .HasName("IX_TutorId");

                entity.Property(e => e.TutorId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TutorName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.TutorStatus)
                    .WithMany(p => p.Tutor)
                    .HasForeignKey(d => d.TutorStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TutorStatus_Tutor");

                entity.HasOne(d => d.TutorType)
                    .WithMany(p => p.Tutor)
                    .HasForeignKey(d => d.TutorTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TutorType_Tutor");
                });

            modelBuilder.Entity<TutorStatus>(entity =>
            {
                entity.Property(e => e.TutorStatusDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TutorType>(entity =>
            {
                entity.Property(e => e.TutorTypeDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
