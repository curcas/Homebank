using System.Collections.Generic;
using System.Linq;
using Homebank.Entities;
using Homebank.Repositories;
using System.Web.Mvc;
using Template = Homebank.Web.Models.TemplateRendering.Template;

namespace Homebank.Web.Controllers
{
    public class BaseController : Controller
    {
	    private readonly UserRepository _userRepository;
	    private readonly TemplateRepository _templateRepository;
	    private readonly AccountRepository _accountRepository;

	    public BaseController(UserRepository userRepository, TemplateRepository templateRepository, AccountRepository accountRepository)
	    {
		    _userRepository = userRepository;
		    _templateRepository = templateRepository;
		    _accountRepository = accountRepository;
	    }

	    public User HomebankUser
	    {
		    get
		    {
			    if (!string.IsNullOrEmpty(User.Identity.Name))
			    {
				    return _userRepository.Get(int.Parse(User.Identity.Name));
			    }

			    return null;
		    }
	    }

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
	    {
		    if (!string.IsNullOrEmpty(User.Identity.Name))
		    {
			    filterContext.Controller.ViewBag.Username = HomebankUser.Name;
				GetTemplates();
		    }

			base.OnActionExecuted(filterContext);
	    }

	    private void GetTemplates()
	    {
		    IList<Template> templateList = new List<Template>();
		    var accounts = _accountRepository.GetAllByUser(HomebankUser, true);

		    foreach (var account in accounts)
		    {
				templateList.Add(new Template
				{
					Name = account.Name,
					Link = Url.Action("Add", "Transaction", new { id = account.Id })
				});

			    foreach (var template in _templateRepository.GetAllByAccount(account.Id))
			    {
					templateList.Add(new Template
					{
						Name = template.Name,
						Link = Url.Action("Add", "Transaction", new { id = template.Account.Id, template = template.Id })
					});
			    }

			    if (accounts.Last().Id != account.Id)
			    {
				    templateList.Add(new Template {IsSeperator = true});
			    }
		    }

		    ViewBag.Templates = templateList;
	    }
    }
}
