
namespace DotCommerce.Persistence.SqlServer.Test
{
	using DotCommerce.API;
	using DotCommerce.Domain;

	public class OrderServiceTests
	{
		/// <summary>
		/// Create new order and save it into database
		/// </summary>
		//public void CreateNewOrder()
		//{
		//	Domain.Order order = new Order("user123");
		//	IPersistence persistence = new SqlServer.Persistence();
		//	OrderService os = new OrderService(persistence);

		//	os.Save(order);
		//}

		/// <summary>
		/// Get order with status incomplete for current user
		/// or if does not exists
		/// create new one
		/// </summary>
		public void GetCreateNewOrder()
		{
			IPersistence persistence = new SqlServer.Persistence();
			IOrderService os = new OrderService(persistence);

			Domain.Order order = os.GetOrCreateOrder("user123");
		}

		public void AddItemToCart()
		{
			IPersistence persistence = new SqlServer.Persistence();
			IOrderService os = new OrderService(persistence);

			Domain.Order order = os.GetOrCreateOrder("user123");
			os.AddToOrder(order.Id, "555", 3, (decimal)10.00, "Test Product 1");
		}
	}
}