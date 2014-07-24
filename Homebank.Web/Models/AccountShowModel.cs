using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Homebank.Entities;

namespace Homebank.Web.Models
{
	public class AccountShowModel
	{
		public IList<Transaction> Transactions { get; set; }
		public Account Account { get; set; }
		public decimal CurrentBalance { get; set; }
		public decimal FutureBalance { get; set; }
		public int TotalTransactions { get; set; }
		public int CurrentPage { get; set; }
	}
}