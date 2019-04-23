using Homebank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homebank.Core.Mappings
{
	public class CategoryMapping : IEntityTypeConfiguration<Category>
	{
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Name");
            builder.Property(p => p.Active).IsRequired().HasColumnName("Active");

            builder.HasOne(p => p.User).WithMany(p => p.Categories).HasForeignKey("User_Id").HasConstraintName("User_Id").IsRequired();
        }
	}
}
