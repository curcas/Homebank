using Homebank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homebank.Core.Mappings
{
	public class TemplateMapping : IEntityTypeConfiguration<Template>
	{
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Name");
            builder.Property(p => p.Description).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Description");
            builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal").HasColumnType("decimal(10, 2)").HasColumnName("Amount");

            builder.HasOne(p => p.Account).WithMany(p => p.Templates).HasForeignKey("Account_Id").HasConstraintName("Account_Id").IsRequired();
            builder.HasOne(p => p.Category).WithMany(p => p.Templates).HasForeignKey("Category_Id").HasConstraintName("Category_Id").IsRequired();
            builder.HasOne(p => p.ReferenceAccount).WithMany(p => p.ReferenceTemplates).HasForeignKey("ReferenceAccount_Id").HasConstraintName("ReferenceAccount_Id").IsRequired(false);
            builder.HasOne(p => p.User).WithMany(p => p.Templates).HasForeignKey("User_Id").HasConstraintName("User_Id").IsRequired();
        }
	}
}
