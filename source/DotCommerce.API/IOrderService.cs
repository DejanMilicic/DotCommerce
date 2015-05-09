namespace DotCommerce.API
{
	using DotCommerce.Domain;

	public interface IOrderService
	{
		Order GetOrCreateOrder(string userId);

		Order Get(int orderId);

		Order AddToOrder(int orderId, string itemId, int quantity, decimal price, 
			string itemName = "", decimal discount = 0, int itemWeight = 0, string itemUrl = "", string imageUrl = "");
	}
}