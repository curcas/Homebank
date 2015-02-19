using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homebank.Repositories;

namespace Homebank.Web.Controllers
{
	public class ReportingController : BaseController
	{
		private readonly ReportingRepository _reportingRepository;
		private readonly CategoryRepository _categoryRepository;
		private readonly AccountRepository _accountRepository;
		private readonly UserRepository _userRepository;
		private readonly TemplateRepository _templateRepository;

		public ReportingController(
			ReportingRepository reportingRepository,
			CategoryRepository categoryRepository,
			AccountRepository accountRepository,
			UserRepository userRepository,
			TemplateRepository templateRepository
			) : base(userRepository, templateRepository, accountRepository)
		{
			_reportingRepository = reportingRepository;
			_categoryRepository = categoryRepository;
			_accountRepository = accountRepository;
			_userRepository = userRepository;
			_templateRepository = templateRepository;
		}

		// GET: Reporting
		public ActionResult Index()
		{
			var x = _reportingRepository.SearchTransactions(
				_categoryRepository.GetAllByUser(HomebankUser, true).Select(p => p.Id),
				_accountRepository.GetAllByUser(HomebankUser, true).Select(p => p.Id),
				new DateTime(2014, 1, 1), 
				new DateTime(2015, 1, 1), 
				ReportingType.Earnings
				);


			return View();
		}
	}
}