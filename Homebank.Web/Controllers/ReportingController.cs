using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Homebank.Entities;
using Homebank.Repositories;
using Homebank.Web.Models;

namespace Homebank.Web.Controllers
{
	public class ReportingController : BaseController
	{
		private readonly ReportingRepository _reportingRepository;
		private readonly CategoryRepository _categoryRepository;
		private readonly AccountRepository _accountRepository;

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
		}

		public ActionResult Index()
		{
			return View(new ReportingModel
			{
				StartDate = DateTime.Now.AddMonths(-1),
				EndDate = DateTime.Now,
				Accounts = GetAllAccounts(),
				Categories = GetAllCategories()
			});
		}

		[HttpPost]
		public ActionResult Index(ReportingModel model)
		{
			Highcharts chart = null;
			IEnumerable<ReportingRecord> transactions = null;

			if (ModelState.IsValid)
			{
				transactions = _reportingRepository.SearchTransactions(HomebankUser, model.Category, model.Account, model.StartDate, model.EndDate, model.ReportingType);

				chart = new Highcharts("report")
					.InitChart(new Chart
					{
						DefaultSeriesType = ChartTypes.Pie,
					}).SetPlotOptions(new PlotOptions
					{
						Pie = new PlotOptionsPie
						{
							AllowPointSelect = true,
							DataLabels = new PlotOptionsPieDataLabels
							{
								Formatter = "function(){ return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) + '% (' + new Number(this.total).toLocaleString() + ')'; }"
							}
						}
					}).SetTitle(new Title
					{
						Text = "Report"
					}).SetTooltip(new Tooltip
					{
						Formatter = "function(){ return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +'% (' + new Number(this.total).toLocaleString() + ')'; }"
					}).SetSeries(new Series
					{
						Type = ChartTypes.Pie,
						Data =
							new Data(
								transactions.GroupBy(p => p.Category)
									.Select(p => new object[] {_categoryRepository.GetById(HomebankUser, p.Key).Name, p.Sum(a => a.Amount)})
									.ToArray())
					});

			}

			return View(new ReportingModel
			{
				Chart = chart,
				Transactions = transactions != null ? transactions.OrderBy(p => p.AccountName).ThenBy(p => p.Date).ThenBy(p => p.Id) : null,
				Accounts = GetAllAccounts(),
				Categories = GetAllCategories()
			});
		}

		[NonAction]
		private Dictionary<int, string> GetAllAccounts()
		{
			var accounts = _accountRepository.GetAllByUser(HomebankUser, true);

			accounts.Add(new Account
			{
				Id = 0,
				Name = "All"
			});

			return accounts.OrderBy(p => p.Name).ToDictionary(a => a.Id, a => a.Name);
		}

		[NonAction]
		private Dictionary<int, string> GetAllCategories()
		{
			var categories = _categoryRepository.GetAllByUser(HomebankUser, true);

			categories.Add(new Category
			{
				Id = 0,
				Name = "All"
			});

			return categories.OrderBy(p => p.Name).ToDictionary(a => a.Id, a => a.Name);
		} 
	}
}