using Homebank.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Homebank.Core.Mappings
{
	public class BookingMapping : EntityTypeConfiguration<Booking>
	{
		public BookingMapping()
		{
			HasKey(p => p.Id);

			Property(p => p.Amount).IsRequired().HasColumnType("decimal").HasPrecision(10, 2).HasColumnName("Amount");
		}
	}
}
