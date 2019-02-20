using Homebank.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Homebank.Core.Mappings
{
	public class AccountMapping : EntityTypeConfiguration<Account>
	{
		public AccountMapping()
		{
			HasKey(p => p.Id);

			Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Name");
			Property(p => p.Active).IsRequired().HasColumnName("Active");
		}
	}
}
