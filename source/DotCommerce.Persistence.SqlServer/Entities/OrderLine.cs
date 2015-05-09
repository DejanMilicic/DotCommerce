
namespace DotCommerce.Persistence.SqlServer.Entities
{
	public class OrderLine
	{
		public int Id { get; set; }
		public string ItemId { get; set; }
		public string ItemName { get; set; }
		public decimal Discount { get; set; }
		public int ItemWeight { get; set; }
		public int Quantity { get; set; }
		public string ImageUrl { get; set; }
		public string ItemUrl { get; set; }
		public decimal ItemPrice { get; set; }

		// Navigation properties
		public Order Order { get; set; }
	}
}
