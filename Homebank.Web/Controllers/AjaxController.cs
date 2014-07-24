using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homebank.Repositories;

namespace Homebank.Web.Controllers
{
	[Authorize]
    public class AjaxController : BaseController
	{
		private readonly TransactionRepository _transactionRepository;

	    public AjaxController(UserRepository userRepository, TemplateRepository templateRepository, TransactionRepository transactionRepository, AccountRepository accountRepository)
		    : base(userRepository, templateRepository, accountRepository)
	    {
		    _transactionRepository = transactionRepository;
	    }

		public ActionResult Autocomplete(string id)
		{
			return Json(_transactionRepository.GetDescriptionsList(HomebankUser, id), JsonRequestBehavior.AllowGet);
		}
    }
}