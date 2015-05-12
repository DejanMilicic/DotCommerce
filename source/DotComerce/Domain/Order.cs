
namespace DotCommerce.Domain
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using DotCommerce.Interfaces;

	class Order : IOrder
	{
		public Order(string userId)
		{
			this.Id = 0;
			this.UserId = userId;
			this.EditableOrderLines = new List<OrderLine>();
			this.Status = OrderStatus.Incomplete;
			this.CreatedOn = DateTime.UtcNow;
			this.LastUpdated = DateTime.UtcNow;
			this.ItemsCount = 0;
			this.Weight = 0;
			this.OrderLinesPrice = 0;
			this.Shipping = 0;
			this.Price = 0;
		}

		public int Id { get; set; }
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

		public void Recalculate(IShippingCalculator shippingCalculator)
		{
			foreach (OrderLine orderLine in this.EditableOrderLines)
			{
				orderLine.Recalculate();
			}

			this.LastUpdated = DateTime.UtcNow;
			this.ItemsCount = this.OrderLines.Sum(orderline => orderline.Quantity);
			this.Weight = this.OrderLines.Sum(orderline => orderline.Weight);
			this.OrderLinesPrice = this.OrderLines.Sum(orderline => orderline.Price);
			this.Shipping = shippingCalculator.GetShuppingCosts(this);
			this.Price = this.OrderLinesPrice + this.Shipping;
		}

		public OrderLine FindOrderLine(string itemId, decimal itemPrice, decimal itemDiscount, int itemWeight)
		{
			return this.EditableOrderLines.FirstOrDefault(ol => ol.ItemId == itemId && ol.ItemPrice == itemPrice
				&& ol.ItemDiscount == itemDiscount && ol.ItemWeight == itemWeight);
		}
	}
}
