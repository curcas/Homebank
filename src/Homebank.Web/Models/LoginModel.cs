using System.ComponentModel.DataAnnotations;
using Homebank.Web.Attributes;

namespace Homebank.Web.Models
{
	public class LoginModel
	{
		[Required(ErrorMessage = "Please enter your username.")]
		[UsernameValid(ErrorMessage = "Username not found.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter your password.")]
		[PasswordValid(ErrorMessage = "Password is invalid.")]
		public string Password { get; set; }

        public string ReturnUrl { get; set; }
	}
}
