using IELTSPayments.Models;
using Microsoft.EntityFrameworkCore;

namespace IELTSPayments.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<IELTSPayment> IELTSPayment { get; set; }

        public DbSet<IELTSTransaction> IELTSTransaction { get; set; }
        public DbSet<StaffMember> StaffMember { get; set; }
        public DbSet<SystemSettings> SystemSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IELTSPayment>()
                .HasKey(c => new { c.TransactionID });

            modelBuilder.Entity<IELTSTransaction>()
                .HasKey(c => new { c.PaymentID, c.TransactionID });

            modelBuilder.Entity<StaffMember>()
                .HasKey(c => new { c.StaffRef });

            modelBuilder.Entity<SystemSettings>()
                .HasKey(c => new { c.Version }); 
        }
    }
}
