using Homebank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homebank.Core.Mappings
{
	public class AccountMapping : IEntityTypeConfiguration<Account>
	{
        public void Configure(EntityTypeBuilder<Account> builder)
        {
			builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Name");
            builder.Property(p => p.Active).IsRequired().HasColumnName("Active");
            builder.Property(p => p.ControlDate).HasColumnType("date").HasColumnName("ControlDate");

            builder.HasOne(p => p.User).WithMany(p => p.Accounts).HasForeignKey("User_Id").HasConstraintName("User_Id").IsRequired();
		}
	}
}
