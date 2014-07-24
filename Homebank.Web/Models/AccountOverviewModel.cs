using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homebank.Web.Models
{
	public class AccountOverviewModel
	{
		public IList<AccountModel> Accounts { get; set; }
	}
}