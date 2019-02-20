namespace Homebank.Core.Entities
{
	public class Template
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }

		public virtual User User { get; set; }
		public virtual Account Account { get; set; }
		public virtual Category Category { get; set; }
		public virtual Account ReferenceAccount { get; set; }
	}
}
