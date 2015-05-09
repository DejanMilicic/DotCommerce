
namespace DotCommerce.API
{
	using System.Linq;

	using DotCommerce.Domain;

	public class OrderService : IOrderService
	{
		private readonly IPersistence persistence;

		public OrderService(IPersistence persistence)
		{
			this.persistence = persistence;
		}

		/// <summary>
		/// For user return order with status incomplete, or create new one otherwise
		/// </summary>
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

		public Order Get(int orderId)
		{
			return this.persistence.Get(orderId);
		}

		public Order AddToOrder(int orderId, string itemId, int quantity, decimal price, 
			string itemName = "", decimal discount = 0, int itemWeight = 0, string itemUrl = "", string imageUrl = "")
		{
			Order order = this.persistence.Get(orderId);
			OrderLine existingOrderLine = order.OrderLines.SingleOrDefault(x => x.ItemId == itemId);

			if (existingOrderLine != null)
			{
				var updatedOrderLine = new OrderLine(id: existingOrderLine.Id, itemId: itemId, itemName: itemName, 
					price: price, quantity: existingOrderLine.Quantity + quantity, discount: discount, 
					itemWeight: itemWeight, imageUrl: imageUrl, itemUrl: itemUrl);

				this.persistence.Save(orderId, updatedOrderLine);
			}
			else
			{
				var newOrderLine = new OrderLine(id: 0, itemId: itemId, itemName: itemName, price: price,
					quantity: quantity, discount: discount, itemWeight: itemWeight,
					imageUrl: imageUrl, itemUrl: itemUrl);

				this.persistence.Save(orderId, newOrderLine);
			}

			return this.persistence.Get(orderId);
		}
	}
}
