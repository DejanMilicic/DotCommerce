
namespace DotCommerce.API
{
	using System.Linq;

	using DotCommerce.Domain;

	public class OrderService
	{
		private readonly IPersistence persistence;

		public OrderService(IPersistence persistence)
		{
			this.persistence = persistence;
		}

		public void Save(Order order)
		{
			this.persistence.Save(order);
		}

		public Order GetOrCreateOrder(string userId)
		{
			Order order = this.persistence.Get(userId, OrderStatus.Incomplete, 1).FirstOrDefault();

			if (order == null)
			{
				order = new Order(userId);
				this.persistence.Save(order);
			}

			return order;
		}
	}
}
