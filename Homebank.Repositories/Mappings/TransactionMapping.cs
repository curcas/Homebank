using Homebank.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Homebank.Repositories.Mappings
{
	public class TransactionMapping : EntityTypeConfiguration<Transaction>
	{
		public TransactionMapping()
		{
			HasKey(p => p.Id);

			Property(p => p.Description).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Description");
			Property(p => p.Date).IsRequired().HasColumnType("datetime").HasColumnName("Date");

			HasMany(p => p.Bookings).WithRequired(p => p.Transaction).WillCascadeOnDelete();
		}
	}
}
