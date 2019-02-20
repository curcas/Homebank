using System.Collections.Generic;

namespace Homebank.Entities
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Active { get; set; }

		public virtual User User { get; set; }
		public virtual IList<Transaction> Transactions { get; set; }
		public virtual IList<Template> Templates { get; set; } 
	}
}
