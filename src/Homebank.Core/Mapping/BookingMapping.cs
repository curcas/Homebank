using Homebank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homebank.Core.Mappings
{
	public class BookingMapping : IEntityTypeConfiguration<Booking>
	{
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal").HasColumnType("decimal(10, 2)").HasColumnName("Amount");

            builder.HasOne(p => p.Account).WithMany(p => p.Bookings).HasForeignKey("Account_Id").HasConstraintName("Account_Id").IsRequired();
            builder.HasOne(p => p.Transaction).WithMany(p => p.Bookings).HasForeignKey("Transaction_Id").HasConstraintName("Transaction_Id").IsRequired();
        }
	}
}
