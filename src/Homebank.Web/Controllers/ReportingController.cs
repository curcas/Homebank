using System;
using System.Collections.Generic;
using System.Linq;
using Homebank.Core.Interfaces.Repositories;
using Homebank.Core.Repositories;
using Homebank.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Homebank.Web.Controllers
{
    public class ReportingController : BaseController
    {
        private readonly IReportingRepository _reportingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;

        public ReportingController(IReportingRepository reportingRepository, ICategoryRepository categoryRepository, IAccountRepository accountRepository, IUserRepository userRepository)
            : base(userRepository)
        {
            _reportingRepository = reportingRepository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
        }

        public ActionResult Index()
        {
            return View(new ReportingModel
            {
                StartDate = DateTime.Now.AddMonths(-1),
                EndDate = DateTime.Now,
                ReportingTypes = GetReportTypes(),
                Accounts = GetAllAccounts(),
                Categories = GetAllCategories()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ReportingModel model)
        {
            if (ModelState.IsValid)
            {
                var transactions = _reportingRepository.SearchTransactions(HomebankUser, model.Category, model.Account, model.StartDate, model.EndDate, model.IncludeTransactionsToOtherAccounts, model.ReportingType);
                var gridData = transactions.GroupBy(p => p.Category).Select(p => new ReportGroup(_categoryRepository.GetById(HomebankUser, p.Key).Name, p.Sum(a => a.Amount))).ToList();

                model.GridData = gridData;
                model.Transactions = transactions.OrderBy(p => p.AccountName).ThenBy(p => p.Date).ThenBy(p => p.Id);
            }


            model.ReportingTypes = GetReportTypes();
            model.Accounts = GetAllAccounts();
            model.Categories = GetAllCategories();

            return View(model);
        }

        [NonAction]
        private IEnumerable<SelectListItem> GetAllAccounts()
        {
            var accounts = _accountRepository.GetAllByUser(HomebankUser, true);

            var list = new List<SelectListItem>
            {
                new SelectListItem("All", "0")
            };

            list.AddRange(accounts.Select(p => new SelectListItem(p.Name, p.Id.ToString())).OrderBy(p => p.Text));

            return list;
        }

        [NonAction]
        private IEnumerable<SelectListItem> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllByUser(HomebankUser, true);

            var list = new List<SelectListItem>
            {
                new SelectListItem("All", "0")
            };

            list.AddRange(categories.Select(p => new SelectListItem(p.Name, p.Id.ToString())).OrderBy(p => p.Text));

            return list;
        }

        [NonAction]
        private IEnumerable<SelectListItem> GetReportTypes()
        {
            return new List<SelectListItem> {
                new SelectListItem(Enum.GetName(typeof(ReportingType), ReportingType.Earnings), ReportingType.Earnings.ToString()),
                new SelectListItem(Enum.GetName(typeof(ReportingType), ReportingType.Expenses), ReportingType.Expenses.ToString())
            };
        }
    }
}