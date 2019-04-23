using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homebank.Core.Entities
{
	public class User
	{
        //[Key]
		public int Id { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string Name { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string Password { get; set; }

        //[Required]
        //[MaxLength(255)]
        public string Salt { get; set; }

        //[InverseProperty(nameof(Account.User))]
        public virtual ICollection<Account> Accounts { get; set; }

        //[InverseProperty(nameof(Template.User))]
        public virtual ICollection<Template> Templates { get; set; }

        //[InverseProperty(nameof(Category.User))]
        public virtual ICollection<Category> Categories { get; set; }
    }
}