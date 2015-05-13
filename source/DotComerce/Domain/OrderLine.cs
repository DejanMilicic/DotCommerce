
namespace DotCommerce.Domain
{
	using DotCommerce.Interfaces;

	class OrderLine : IOrderLine
	{
		public int Id { get; set; }
		public string ItemId { get; set; }
		public string ItemName { get; set; }
		public decimal ItemDiscount { get; set; }
		public int ItemWeight { get; set; }
		public string ItemImageUrl { get; set; }
		public string ItemUrl { get; set; }
		public decimal ItemPrice { get; set; }
		public int Quantity { get; set; }
		public int Weight { get; set; }
		public decimal Price { get; set; }
	}
}
