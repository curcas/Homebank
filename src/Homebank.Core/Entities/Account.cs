using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homebank.Core.Entities
{
	public class Account
	{
        //[Key]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string Name { get; set; }

        public bool Active { get; set; }
        public DateTime? ControlDate { get; set; }

        //[Required]
        //[ForeignKey("User_Id")]
        public virtual User User { get; set; }

        //[InverseProperty(nameof(Template.Account))]
        public virtual ICollection<Template> Templates { get; set; }

        //[InverseProperty(nameof(Template.ReferenceAccount))]
        public virtual ICollection<Template> ReferenceTemplates { get; set; }

        //[InverseProperty(nameof(Booking.Account))]
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
