using System;
using System.Collections.Generic;

namespace Homebank.Entities
{
	public class Transaction
	{
		public Transaction()
		{
			Bookings = new List<Booking>();
		}

		public int Id { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }

		public virtual Category Category { get; set; }
		public virtual IList<Booking> Bookings { get; set; }
	}
}
