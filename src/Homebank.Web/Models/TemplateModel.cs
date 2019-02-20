using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Homebank.Web.Attributes;

namespace Homebank.Web.Models
{
	public class TemplateModel
	{
		public TemplateModel()
		{
			Accounts = new Dictionary<int, string>();
			Categories = new Dictionary<int, string>();
			ReferenceAccounts = new Dictionary<int, string>();
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter a name.")]
		[MaxLength(255, ErrorMessage = "Name cannot be longer than 255 characters.")]
		[TemplateNameUnique("Id", ErrorMessage = "A template for this account with this name already exists.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter a description.")]
		[MaxLength(255, ErrorMessage = "Name cannot be longer than 255 characters.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Please enter an amount.")]
		public decimal Amount { get; set; }

		[Required(ErrorMessage = "Please enter choose an account.")]
		[NotEqualTo("ReferenceAccountId", ErrorMessage = "Account is not allowed to be the same as reference account.")]
		public int AccountId { get; set; }

		[NotEqualTo("AccountId", ErrorMessage = "Reference account is not allowed to be the same as account.")]
		public int ReferenceAccountId { get; set; }

		[Required(ErrorMessage = "Please enter choose a category.")]
		public int CategoryId { get; set; }

		public Dictionary<int, string> Accounts { get; set; }

		public Dictionary<int, string> ReferenceAccounts { get; set; } 

		public Dictionary<int, string> Categories { get; set; } 
	}
}