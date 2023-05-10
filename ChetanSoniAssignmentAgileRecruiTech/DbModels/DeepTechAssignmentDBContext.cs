using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChetanSoniAssignmentAgileRecruiTech.DbModels
{
    public partial class DeepTechAssignmentDBContext : DbContext
    {
        public DeepTechAssignmentDBContext()
        {
        }

        public DeepTechAssignmentDBContext(DbContextOptions<DeepTechAssignmentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAttendee> TblAttendees { get; set; } = null!;
        public virtual DbSet<TblCategory> TblCategories { get; set; } = null!;
        public virtual DbSet<TblEvent> TblEvents { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS; Database=DeepTechAssignmentDB; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAttendee>(entity =>
            {
                entity.ToTable("tbl_attendees");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.TblAttendees)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_tbl_attendees_tbl_events");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblAttendees)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tbl_attendees_tbl_users");
            });

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__tbl_cate__D54EE9B436C1FE55");

                entity.ToTable("tbl_category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("category_name");
            });

            modelBuilder.Entity<TblEvent>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK__tbl_even__2370F72721488A51");

                entity.ToTable("tbl_events");

                entity.Property(e => e.EventId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("event_id");

                entity.Property(e => e.Category).HasColumnName("category");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Moderator).HasColumnName("moderator");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.RigorRank).HasColumnName("rigor_rank");

                entity.Property(e => e.Schedule)
                    .HasColumnType("datetime")
                    .HasColumnName("schedule");

                entity.Property(e => e.SubCategory).HasColumnName("sub_category");

                entity.Property(e => e.Tagline)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("tagline");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.TblEventCategoryNavigations)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK_tbl_events_tbl_category");

                entity.HasOne(d => d.Event)
                    .WithOne(p => p.TblEventEvent)
                    .HasForeignKey<TblEvent>(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_events_tbl_users");

                entity.HasOne(d => d.ModeratorNavigation)
                    .WithMany(p => p.TblEventModeratorNavigations)
                    .HasForeignKey(d => d.Moderator)
                    .HasConstraintName("FK_tbl_events_tbl_users1");

                entity.HasOne(d => d.SubCategoryNavigation)
                    .WithMany(p => p.TblEventSubCategoryNavigations)
                    .HasForeignKey(d => d.SubCategory)
                    .HasConstraintName("FK_tbl_events_tbl_category1");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tbl_user__B9BE370F547AB172");

                entity.ToTable("tbl_users");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
