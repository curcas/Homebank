using Homebank.Web.Attributes;
using System;
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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DateRange(Nullable = true, ErrorMessage = "Date must be between 1900-01-01 and 2100-01-01")]
        [DataType(DataType.Text)]
        public DateTime? ControlDate { get; set; }
	}
}