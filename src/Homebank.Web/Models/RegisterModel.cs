using System.ComponentModel.DataAnnotations;
using Homebank.Web.Attributes;

namespace Homebank.Web.Models
{
	public class RegisterModel
	{
		[Required(ErrorMessage = "Please enter your desired username.")]
		[MaxLength(255, ErrorMessage = "Name cannot be longer than 255 characters.")]
		[UniqueUsername(null, ErrorMessage = "Username is already in use.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter a password.")]
		[Compare("ConfirmPassword", ErrorMessage = "The passwords doesn't match")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Please enter a password.")]
		public string ConfirmPassword { get; set; }
	}
}