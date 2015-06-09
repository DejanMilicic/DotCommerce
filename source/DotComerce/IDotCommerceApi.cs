
namespace DotCommerce
{
	using System;

	using DotCommerce.Domain;
	using DotCommerce.Interfaces;

	public interface IDotCommerceApi
	{
		/// <summary>
		/// For user return order with status incomplete, or create new one otherwise
		/// </summary>
		IOrder GetIncompleteOrder(string userId);

		IOrder Get(Guid orderId);

		void AddItemToOrder(IOrder order, string itemid, int quantity, decimal price, 
			string name = "", decimal discount = 0, int weight = 0, string url = "", string imageUrl = "");

		void RemoveOrderLine(int orderLineId);

		void ChangeQuantity(int orderLineId, int quantity);

		void SetShippingAddress(IOrder order, 
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

		void SetBillingAddress(IOrder order, 
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

		void SetStatus(IOrder order, OrderStatus status);

		void SetUser(IOrder order, string userId);
	}
}