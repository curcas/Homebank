using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homebank.Core.Entities
{
	public class Booking
	{
		public int Id { get; set; }
		public decimal Amount { get; set; }

		public virtual Account Account { get; set; }
		public virtual Transaction Transaction { get; set; }
	}
}
