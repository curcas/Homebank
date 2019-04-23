using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homebank.Core.Entities
{
	public class Template
	{
        //[Key]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string Name { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string Description { get; set; }

        //[Required]
        public decimal Amount { get; set; }

        //[Required]
        //[ForeignKey("User_Id")]
        public virtual User User { get; set; }

        //[Required]
        //[ForeignKey("Account_Id")]
        public virtual Account Account { get; set; }

        //[Required]
        //[ForeignKey("Category_Id")]
        public virtual Category Category { get; set; }

        //[ForeignKey("ReferenceAccount_Id")]
        public virtual Account ReferenceAccount { get; set; }
	}
}
