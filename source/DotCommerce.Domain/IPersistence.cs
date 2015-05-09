namespace DotCommerce.Domain
{
	using System.Collections.Generic;

	public interface IPersistence
	{
		void Save(Order order);

		List<Domain.Order> Get(string userId, OrderStatus orderStatus, int take);

		Order Get(int orderId);

		void Save(int orderId, OrderLine orderLine);
	}
}