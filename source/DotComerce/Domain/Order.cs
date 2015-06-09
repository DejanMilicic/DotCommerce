
namespace DotCommerce.Domain
{
	using System;
	using System.Collections.Generic;
	using DotCommerce.Interfaces;

	class Order : IOrder
	{
		public Order(string userId)
		{
			this.Id = Guid.NewGuid();
			this.UserId = userId;
			this.EditableOrderLines = new List<OrderLine>();
			this.Status = OrderStatus.Incomplete;
			this.EditableOrderLogs = new List<OrderLog>();
		}

		public Guid Id { get; set; }
		public string UserId { get; set; }
		public IEnumerable<IOrderLine> OrderLines 
		{
			get
			{
				return this.EditableOrderLines;
			} 
		}
		public List<OrderLine> EditableOrderLines { get; set; } 
		public OrderStatus Status { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime LastUpdated { get; set; }
		public int ItemsCount { get; set; }
		public int Weight { get; set; }
		public decimal OrderLinesPrice { get; set; } 
		public decimal Shipping { get; set; }
		public decimal Price { get; set; }

		public IOrderAddress ShippingAddress { get { return EditableShippingAddress; } }
		public OrderAddress EditableShippingAddress { get; set; }

		public IOrderAddress BillingAddress { get { return EditableBillingAddress; } }


		public IEnumerable<IOrderLog> OrderLogs
		{
			get
			{
				return this.EditableOrderLogs;
			}
		}
		public List<OrderLog> EditableOrderLogs { get; set; } 

		public OrderAddress EditableBillingAddress { get; set; }
	}
}
