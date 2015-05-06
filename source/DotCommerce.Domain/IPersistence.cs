namespace DotCommerce.Domain
{
	using System.Collections.Generic;

	public interface IPersistence
	{
		void Save(Order order);

		List<Domain.Order> Get(string userId, OrderStatus orderStatus, int take);
	}
}