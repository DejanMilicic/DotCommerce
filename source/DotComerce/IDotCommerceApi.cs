
namespace DotCommerce
{
	using DotCommerce.Domain;
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

		IOrder RemoveOrderLine(int orderLineId);

		IOrder ChangeQuantity(int orderLineId, int quantity);

		IOrder SetShippingAddress(int orderId, 
			string title = "",
			string firstName = "",
			string lastName = "",
			string company = "",
			string street = "",
			string streetNumber = "",
			string city = "",
			string zip = "",
			string country = "",
			string state = "",
			string province = "",
			string email = "",
			string phone = "", 
			bool singleAddress =  false
			);

		IOrder SetBillingAddress(int orderId, 
			string title = "",
			string firstName = "",
			string lastName = "",
			string company = "",
			string street = "",
			string streetNumber = "",
			string city = "",
			string zip = "",
			string country = "",
			string state = "",
			string province = "",
			string email = "",
			string phone = "", 
			bool singleAddress =  false
			);

		IOrder SetStatus(int orderId, OrderStatus status);

		IOrder SetUser(int orderId, string userId);
	}
}