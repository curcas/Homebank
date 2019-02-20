using Homebank.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Homebank.Core.Mappings
{
	public class TemplateMapping : EntityTypeConfiguration<Template>
	{
		public TemplateMapping()
		{
			HasKey(p => p.Id);

			Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Name");
			Property(p => p.Description).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Description");
			Property(p => p.Amount).IsRequired().HasColumnType("decimal").HasPrecision(10, 2).HasColumnName("Amount");
		}
	}
}
