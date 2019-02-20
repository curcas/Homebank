using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homebank.Web.Models.TemplateRendering
{
	public class Template
	{
		public string Name { get; set; }
		public string Link { get; set; }
		public bool IsSeperator { get; set; }
	}
}