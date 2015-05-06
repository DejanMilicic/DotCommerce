
namespace DotCommerce.Persistence.SqlServer.Entities
{
	public class OrderLine
	{
		public int Id { get; set; }
		public string ItemId { get; set; }
		public string ItemName { get; set; }

		public Order Order { get; set; }
	}
}
