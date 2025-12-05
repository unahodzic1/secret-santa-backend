using SecretSantaBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SecretSantaBackend.Data
{

    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Pair> Pairs { get; set; }
        public DbSet<SecretSantaList> SecretSantaLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasOne(e => e.User).WithOne(u => u.Employee!).HasForeignKey<Employee>(e => e.UserId).IsRequired(false);

            modelBuilder.Entity<Pair>().HasOne(p => p.Giver).WithMany(e => e.DrawnPair).HasForeignKey(p => p.GiverId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pair>().HasOne(p => p.Receiver).WithMany(e => e.GiverPair).HasForeignKey(p => p.ReceiverId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pair>().HasOne(p => p.List).WithMany(l => l.Pairs).HasForeignKey(p => p.ListId).IsRequired();
        }
    }
}
