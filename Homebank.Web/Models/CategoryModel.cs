using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Homebank.Web.Attributes;

namespace Homebank.Web.Models
{
	public class CategoryModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter a name.")]
		[CategoryNameUnique("Id", ErrorMessage = "A category with this name already exists.")]
		[MaxLength(255, ErrorMessage = "Name cannot be longer than 255 characters.")]
		public string Name { get; set; }

		public bool Active { get; set; }
	}
}