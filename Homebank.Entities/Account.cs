namespace Homebank.Entities
{
	public class Account
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Active { get; set; }

		public virtual User User { get; set; }
	}
}
