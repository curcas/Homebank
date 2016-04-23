using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Homebank.Web.Attributes;

namespace Homebank.Web.Models
{
	public class TransactionModel
	{
		public TransactionModel()
		{
			Categories = new Dictionary<int, string>();
			ReferenceAccounts = new Dictionary<int, string>();
		}

		[Required]
		public string AccountName { get; set; }

		[Required]
		public int DataId { get; set; }

		[Required(ErrorMessage = "Please enter a description")]
		[MaxLength(255, ErrorMessage = "Description cannot be longer than 255 characters.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Please enter an amount")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
		[NotZero(ErrorMessage = "Amount cannot be zero")]
		public decimal Amount { get; set; }

		[Required(ErrorMessage = "Please enter a date")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DateRange(ErrorMessage = "Date must be between 1900-01-01 and 2100-01-01")]
		[DataType(DataType.Text)]
		public DateTime Date { get; set; }

		[Required]
		public int AccountId { get; set; }

		public Dictionary<int, string> Categories { get; set; }
		public int CategoryId { get; set; }

		public Dictionary<int, string> ReferenceAccounts { get; set; }
		public int? ReferenceAccountId { get; set; }
	}
}