using Homebank.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Homebank.Repositories.Mappings
{
	public class UserMapping : EntityTypeConfiguration<User>
	{
		public UserMapping()
		{
			HasKey(p => p.Id);

			Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Name");
			Property(p => p.Password).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Password");
			Property(p => p.Salt).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Salt");
		}
	}
}
