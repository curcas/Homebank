using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homebank.Core.Entities;
using Homebank.Core.Interfaces.Repositories;

namespace Homebank.Core.Repositories
{
	public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
		public BookingRepository(DatabaseContext databaseContext)
			: base(databaseContext)
		{
		}
	}
}
