using System.Web.Mvc;
using Homebank.Core.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class UsernameValidAttribute : ValidationAttribute
	{

		public override bool IsValid(object value)
		{
			var userRepository = (UserRepository)DependencyResolver.Current.GetService(typeof(UserRepository));
			return userRepository.Get((string)value) != null;
		}
	}
}