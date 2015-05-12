
namespace DotCommerce
{
	using DotCommerce.Interfaces;

	public interface IDotCommerceApi
	{
		/// <summary>
		/// For user return order with status incomplete, or create new one otherwise
		/// </summary>
		IOrder GetOrCreateOrder(string userId);

		IOrder Get(int orderId);

		IOrder AddItemToOrder(int orderId, string itemid, int quantity, decimal price, 
			string name = "", decimal discount = 0, int weight = 0, string url = "", string imageUrl = "");
	}
}