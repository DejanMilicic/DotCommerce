
namespace DotCommerce.Persistence.SqlServer.Test
{
	using DotCommerce.Interfaces;

	public class OrderServiceTests
	{
		public void AddItemToCart()
		{
			IDotCommerceApi dc = new DotCommerceApi(new ShippingCalculator());
			IOrder order = dc.GetOrCreateOrder("user123");
			dc.AddItemToOrder(order.Id, "666", 3, (decimal)10.00, "Test Product 1");
		}
	}
}