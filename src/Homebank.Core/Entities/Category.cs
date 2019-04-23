using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homebank.Core.Entities
{
	public class Category
	{
        //[Key]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string Name { get; set; }

        public bool Active { get; set; }

        //[Required]
        //[ForeignKey("User_Id")]
        public virtual User User { get; set; }

        //[InverseProperty(nameof(Transaction.Category))]
        public virtual ICollection<Transaction> Transactions { get; set; }

        //[InverseProperty(nameof(Template.Category))]
        public virtual ICollection<Template> Templates { get; set; } 
	}
}
