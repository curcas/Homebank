using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homebank.Entities;

namespace Homebank.Repositories
{
	public class BookingRepository : BaseRepository<Booking>
	{
		public BookingRepository(DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}
	}
}
