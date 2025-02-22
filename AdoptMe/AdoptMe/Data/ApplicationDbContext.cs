using AdoptMe.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdoptMe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Pet> Pets { get; set; } = default!;
        public virtual DbSet<Vet> Vet { get; set; } = default!;
        public virtual DbSet<Visit> Visits { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("database");

            builder.Entity<Visit>()
                .HasKey(v => new { v.PetId, v.VetId });

            builder.Entity<Visit>()
                .HasOne(v => v.Pet)
                .WithMany(c => c.Visits)
                .HasForeignKey(v => v.PetId);

            builder.Entity<Visit>()
                .HasOne(v => v.Vet)
                .WithMany(vet => vet.Visits)
                .HasForeignKey(v => v.VetId);

        }
    }
}
