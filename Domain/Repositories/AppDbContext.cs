using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Repositories
{
    public class AppDbContext : DbContext
    {

        public AppDbContext([NotNull] DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            MapUsers(builder);
        }

        private void MapUsers(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("users");

            MapBaseEntityFields(builder.Entity<User>());

            builder.Entity<User>()
                   .Property(u => u.Email)
                   .HasColumnName("email")
                   .IsRequired();
            builder.Entity<User>()
                   .Property(u => u.Pseudo)
                   .HasColumnName("pseudo")
                   .IsRequired();
            builder.Entity<User>()
                   .Property(u => u.PasswordHash)
                   .HasColumnName("password_hash")
                   .IsRequired();
            builder.Entity<User>()
                   .Property(u => u.PasswordSalt)
                   .HasColumnName("password_salt")
                   .IsRequired();
        }

        private void MapBaseEntityFields<T>(EntityTypeBuilder<T> entityBuilder) where T : BaseEntity
        {
            entityBuilder.Property(x => x.Id)
                         .HasColumnName("id")
                         .IsRequired();
            entityBuilder.Property(x => x.CreatedAt)
                         .HasColumnName("created_at")
                         .IsRequired();
            entityBuilder.Property(x => x.CreatedById)
                         .HasColumnName("created_by")
                         .IsRequired();
            entityBuilder.Property(x => x.UpdatedAt)
                         .HasColumnName("updated_at")
                         .IsRequired();
            entityBuilder.Property(x => x.UpdatedById)
                         .HasColumnName("updated_by")
                         .IsRequired();
        }
    }
}
