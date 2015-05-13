
namespace DotCommerce.Persistence.SqlServer.Test.Infrastructure.DTO
{
	public class Product
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public decimal Discount { get; set; }
		public int Weight { get; set; }
		public string ImageUrl { get; set; }
		public string Url { get; set; }
	}
}