
namespace DotCommerce.Interfaces
{
	using System;
	using System.Collections.Generic;

	using DotCommerce.Domain;

	public interface IOrder
	{
		/// <summary>
		/// Internal Id of the order
		/// </summary>
		Guid Id { get; }

		/// <summary>
		/// Id of a user. Can be primary key of a user in an external system 
		/// or a cookie guid for unauthenticated users
		/// </summary>
		string UserId { get; }

		/// <summary>
		/// Read-only collection of order lines holding items contained in an order
		/// </summary>
		IEnumerable<IOrderLine> OrderLines { get; }

		OrderStatus Status { get; }

		DateTime CreatedOn { get; }

		DateTime LastUpdated { get; }

		int ItemsCount { get; }

		int Weight { get; }

		decimal OrderLinesPrice { get; } 

		decimal Shipping { get; }

		decimal Price { get; }

		IOrderAddress ShippingAddress { get; }
		
		IOrderAddress BillingAddress { get; }

		IEnumerable<IOrderLog> OrderLogs { get; }

		/// <summary>
		/// Ordinal number of completed order, can be used for invoicing
		/// purposes or for communication with customer, as order reference number
		/// </summary>
		int? Ordinal { get; }
	}
}