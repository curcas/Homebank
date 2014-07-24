using System.Configuration;
using Homebank.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Homebank.Web.Attributes
{
	public class UsernameValidAttribute : ValidationAttribute
	{
		private readonly UserRepository _userRepository;
		private readonly string _IdPropertyName;

		public UsernameValidAttribute()
		{
			_userRepository = new UserRepository(new DatabaseContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString));
		}

		public override bool IsValid(object value)
		{
			return _userRepository.Get((string)value) != null;
		}
	}
}