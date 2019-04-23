using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Homebank.Core.Repositories;
using Homebank.Web.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homebank.Web.Models
{
	public class ReportingModel
	{
        public IList<ReportGroup> GridData;

		[Display(Name = "Show")]
		[Required]
		public ReportingType ReportingType { get; set; }
        public IEnumerable<SelectListItem> ReportingTypes { get; set; }

        [Required(ErrorMessage = "Please enter a start date.")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DateRange(ErrorMessage = "Start date must be between 1900-01-01 and 2100-01-01.")]
		[DataType(DataType.Text, ErrorMessage = "Please enter a valid start date.")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "Please enter an end date.")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DateRange(ErrorMessage = "End date must be between 1900-01-01 and 2100-01-01.")]
		[GreaterThan("StartDate", ErrorMessage = "End date must be greater than start date.")]
		[DataType(DataType.Text, ErrorMessage = "Please enter a valid end date.")]
		public DateTime EndDate { get; set; }

        public IEnumerable<SelectListItem> Accounts { get; set; }

        [Required(ErrorMessage = "Please choose at least one account.")]
		public List<int> Account { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

		[Required(ErrorMessage = "Please choose at least one category.")]
		public List<int> Category { get; set; }

		public bool IncludeTransactionsToOtherAccounts { get; set; }

		public IEnumerable<ReportingRecord> Transactions { get; set; } 
	}

    public struct ReportGroup
    {
        public ReportGroup(string name, decimal value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public decimal Value { get; }
    }
}
