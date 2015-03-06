using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNet.Highcharts;
using Foolproof;
using Homebank.Repositories;
using Homebank.Web.Attributes;

namespace Homebank.Web.Models
{
	public class ReportingModel
	{
		public Highcharts Chart { get; set; }

		[Display(Name = "Show")]
		[Required]
		public ReportingType ReportingType { get; set; }

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

		public Dictionary<int, string> Accounts { get; set; }

		[Required(ErrorMessage = "Please choose at least one account.")]
		public List<int> Account { get; set; }

		public Dictionary<int, string> Categories { get; set; }

		[Required(ErrorMessage = "Please choose at least one category.")]
		public List<int> Category { get; set; }

		public bool IncludeTransactionsToOtherAccounts { get; set; }

		public IEnumerable<ReportingRecord> Transactions { get; set; } 
	}
}
