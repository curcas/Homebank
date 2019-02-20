using Homebank.Core.Entities;
using Homebank.Core.Mappings;
using System.Data.Entity;

namespace Homebank.Core.Repositories
{
	public class DatabaseContext : DbContext
	{
		/// <summary>
		/// Initializes the DB context.
		/// </summary>
		/// <param name="connectionString">The name of the connection string or the connection string itself.</param>
		public DatabaseContext(string connectionString)
			: base(connectionString)
		{
			Database.SetInitializer(new NullDatabaseInitializer<DatabaseContext>());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(new UserMapping());
			modelBuilder.Configurations.Add(new CategoryMapping());
			modelBuilder.Configurations.Add(new AccountMapping());
			modelBuilder.Configurations.Add(new TransactionMapping());
			modelBuilder.Configurations.Add(new BookingMapping());
			modelBuilder.Configurations.Add(new TemplateMapping());
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Template> Templates { get; set; }
	}
}
