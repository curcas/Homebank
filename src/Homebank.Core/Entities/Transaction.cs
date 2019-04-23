using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homebank.Core.Entities
{
	public class Transaction
	{
        //[Key]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string Description { get; set; }

        //[Required]
        public DateTime Date { get; set; }

        //[Required]
        //[ForeignKey("Category_Id")]
        public virtual Category Category { get; set; }

        //[InverseProperty(nameof(Booking.Transaction))]
        public virtual ICollection<Booking> Bookings { get; set; }
	}
}
