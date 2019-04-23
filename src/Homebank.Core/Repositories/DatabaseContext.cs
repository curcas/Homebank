using Homebank.Core.Entities;
using Homebank.Core.Mappings;
using Microsoft.EntityFrameworkCore;
namespace Homebank.Core.Repositories
{
	public class DatabaseContext : DbContext
	{
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new AccountMapping());
            modelBuilder.ApplyConfiguration(new TransactionMapping());
            modelBuilder.ApplyConfiguration(new BookingMapping());
            modelBuilder.ApplyConfiguration(new TemplateMapping());
        }

        public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Template> Templates { get; set; }
	}
}
