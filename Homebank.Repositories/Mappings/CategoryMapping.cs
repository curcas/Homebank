using Homebank.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Homebank.Repositories.Mappings
{
	public class CategoryMapping : EntityTypeConfiguration<Category>
	{
		public CategoryMapping()
		{
			HasKey(p => p.Id);

			Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Name");
			Property(p => p.Active).IsRequired().HasColumnName("Active");
		}
	}
}
