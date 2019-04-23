using Homebank.Core.Interfaces.Repositories;
using Homebank.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homebank.Web.Controllers
{
	[Authorize]
    public class AjaxController : BaseController
	{
		private readonly ITransactionRepository _transactionRepository;

	    public AjaxController(IUserRepository userRepository, ITransactionRepository transactionRepository)
		    : base(userRepository)
	    {
		    _transactionRepository = transactionRepository;
	    }

		public ActionResult Autocomplete(string id)
		{
			return Json(_transactionRepository.GetDescriptionsList(HomebankUser, id));
		}
    }
}