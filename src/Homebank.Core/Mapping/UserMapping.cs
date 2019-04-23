using Homebank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homebank.Core.Mappings
{
	public class UserMapping : IEntityTypeConfiguration<User>
	{
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Name");
            builder.Property(p => p.Password).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Password");
            builder.Property(p => p.Salt).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Salt");
		}
    }
}
