
namespace DotCommerce.Domain
{
	using System;
	using System.Collections.Generic;

	public class Order
	{
		public Order(string userId)
		{
			this.Id = 0;
			this.UserId = userId;
			this.OrderLines = new List<OrderLine>();
			this.Status = OrderStatus.Incomplete;
			this.CreatedOn = DateTime.UtcNow;
		}

		public Order(int id, string userId, List<OrderLine> orderLines, OrderStatus orderStatus, DateTime createdOn)
		{
			this.Id = id;
			this.UserId = userId;
			this.OrderLines = orderLines;
			this.Status = orderStatus;
			this.CreatedOn = createdOn;
		}

		/// <summary>
		/// Internal Id of the order
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		/// Id of a user. Can be primary key of a user in an external system 
		/// or a cookie guid for unauthenticated users
		/// </summary>
		public string UserId { get; private set; }

		/// <summary>
		/// Read-only collection of order lines holding items contained in an order
		/// </summary>
		public IEnumerable<OrderLine> OrderLines { get; private set; }


		public OrderStatus Status { get; private set; }

		public DateTime CreatedOn { get; private set; }
	}
}
