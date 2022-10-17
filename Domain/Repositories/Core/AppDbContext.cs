using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Repositories.Core
{
    public class AppDbContext : DbContext
    {

        public AppDbContext([NotNull] DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Unit> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            MapUsers(builder);
            MapUnits(builder);
            MapArmies(builder);
        }

        private void MapUsers(ModelBuilder builder)
        {
            var entityBuilder = builder.Entity<User>();
            entityBuilder.ToTable("users");

            MapBaseEntityFields(entityBuilder);

            entityBuilder
                   .Property(u => u.Email)
                   .HasColumnName("email")
                   .IsRequired();
            entityBuilder
                   .Property(u => u.Pseudo)
                   .HasColumnName("pseudo")
                   .IsRequired();
            entityBuilder
                   .Property(u => u.PasswordHash)
                   .HasColumnName("password_hash")
                   .IsRequired();
            entityBuilder
                   .Property(u => u.PasswordSalt)
                   .HasColumnName("password_salt")
                   .IsRequired();
            entityBuilder
                   .Property(u => u.IsAdmin)
                   .HasColumnName("is_admin");
        }

        private void MapUnits(ModelBuilder builder)
        {
            var entityBuilder = builder.Entity<Unit>();
            entityBuilder.ToTable("units");

            entityBuilder
                   .Property(u => u.Id)
                   .HasColumnName("id")
                   .IsRequired();
            entityBuilder
                   .Property(u => u.Name)
                   .HasColumnName("name")
                   .IsRequired();
            entityBuilder
                   .Property(u => u.Stats)
                   .HasColumnName("stats")
                   .HasConversion(v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                                  v => JsonConvert.DeserializeObject<UnitStats>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) ?? new UnitStats())
                   .IsRequired();
        }

        private void MapArmies(ModelBuilder builder)
        {
            var entityBuilder = builder.Entity<Army>();
            entityBuilder.ToTable("armies");

            MapBaseEntityFields(entityBuilder);

            entityBuilder
                   .Property(u => u.UserUnits)
                   .HasColumnName("units")
                   .HasConversion(v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                                  v => JsonConvert.DeserializeObject<ICollection<UserUnit>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) ?? new List<UserUnit>())
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
