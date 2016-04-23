using Homebank.Web.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Models
{
	public class AccountModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter a name.")]
		[AccountNameUnique("Id", ErrorMessage = "An account with this name already exists.")]
		[MaxLength(255, ErrorMessage = "Name cannot be longer than 255 characters.")]
		public string Name { get; set; }

		public bool Active { get; set; }

		public decimal CurrentBalance { get; set; }

		public decimal FutureBalance { get; set; }
	}
}