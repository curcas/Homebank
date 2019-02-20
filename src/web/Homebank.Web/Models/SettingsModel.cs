using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Homebank.Web.Attributes;

namespace Homebank.Web.Models
{
	public class SettingsModel
	{																																	   
		[Required(ErrorMessage = "Please enter a username.")]
		[UniqueUsername("OriginalName", ErrorMessage = "This username as already in use.")]
		public string Name { get; set; }

		public string OriginalName { get; set; }

		[PasswordValid(ErrorMessage = "Password is not valid")]
		[RequiredIfNotEmpty("NewPassword", ErrorMessage = "You need to enter your old password in order to change it.")]
		public string OldPassword { get; set; }

		[Compare("NewPasswordRepeat", ErrorMessage = "The new passwords doesn't match")]
		public string NewPassword { get; set; }

		public string NewPasswordRepeat { get; set; }
	}
}