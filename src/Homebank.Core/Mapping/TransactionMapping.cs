using Homebank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homebank.Core.Mappings
{
	public class TransactionMapping : IEntityTypeConfiguration<Transaction>
	{
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Description).IsRequired().HasColumnType("nvarchar").HasMaxLength(255).HasColumnName("Description");
            builder.Property(p => p.Date).IsRequired().HasColumnType("datetime").HasColumnName("Date");

            builder.HasOne(p => p.Category).WithMany(p => p.Transactions).HasForeignKey("Category_Id").HasConstraintName("Category_Id").IsRequired();
        }
	}
}
