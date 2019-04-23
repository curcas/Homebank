using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homebank.Core.Entities
{
	public class Booking
	{
        //[Key]
        public int Id { get; set; }

        //[Required]
        public decimal Amount { get; set; }

        //[Required]
        //[ForeignKey("Account_Id")]
        public virtual Account Account { get; set; }

        //[Required]
        //[ForeignKey("Transaction_Id")]
        public virtual Transaction Transaction { get; set; }
	}
}
