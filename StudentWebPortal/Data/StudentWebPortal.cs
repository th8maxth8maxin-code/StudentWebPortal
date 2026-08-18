using Microsoft.EntityFrameworkCore;
using StudentWebPortal.Model.Entity;

namespace StudentWebPortal.Data
{
    public class StudentWebPortalContext(DbContextOptions<StudentWebPortalContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<HifzSession> HifzSessions { get; set; }
        public DbSet<RevisionSession> RevisionSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique()
                .HasFilter("\"Email\" IS NOT NULL");

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasIndex(a => new { a.StudentId, a.AttendanceDate })
                    .IsUnique();

                entity.HasOne(a => a.Student)
                    .WithMany()
                    .HasForeignKey(a => a.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(a => a.CreatedAt)
                    .HasDefaultValueSql("now() AT TIME ZONE 'utc'")
                    .ValueGeneratedOnAdd();

                entity.Property(a => a.SessionStatus)
                    .HasConversion<string>()
                    .HasMaxLength(20);
            });
        }

        public override int SaveChanges()
        {
            ApplyAuditTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditTimestamps()
        {
            foreach (var entry in ChangeTracker.Entries<Student>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            foreach (var entry in ChangeTracker.Entries<Attendance>())
            {
                if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                // CreatedAt for Attendance is handled by the DB default (HasDefaultValueSql),
                // so no need to set it here.
            }

            foreach (var entry in ChangeTracker.Entries<HifzSession>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}