
namespace DotCommerce.Infrastructure.EntityFramework.Entities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;
	using AutoMapper;
	using DotCommerce.Domain;
	using DotCommerce.Interfaces;

	[Table("DotCommerce_Order")]
	class EfOrder
	{
		public EfOrder()
		{

		}

		public EfOrder(string userId, Guid guid)
		{
			this.Id = guid;
			this.UserId = userId;
			this.OrderLines = new List<EfOrderLine>();
			this.Status = OrderStatus.Incomplete.ToString();
			this.CreatedOn = DateTime.UtcNow;
			this.LastUpdated = DateTime.UtcNow;
			this.ItemsCount = 0;
			this.Weight = 0;
			this.OrderLinesPrice = 0;
			this.Shipping = 0;
			this.Price = 0;
		}

		public Guid Id { get; set; }
		public string UserId { get; set; }
		public List<EfOrderLine> OrderLines { get; set; }
		public string Status { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime LastUpdated { get; set; }
		public int ItemsCount { get; set; }
		public int Weight { get; set; }
		public decimal OrderLinesPrice { get; set; } 
		public decimal Shipping { get; set; }
		public decimal Price { get; set; }
		public int? Ordinal { get; set; }
		public EfAddress ShippingAddress { get; set; }
		public EfAddress BillingAddress { get; set; }
		public string Notes { get; set; }
		public List<EfOrderLog> OrderLogs { get; set; }

		public EfOrderLine FindOrderLine(string itemId, decimal itemPrice, decimal itemDiscount, int itemWeight)
		{
			return this.OrderLines.FirstOrDefault(ol => ol.ItemId == itemId && ol.ItemPrice == itemPrice
				&& ol.ItemDiscount == itemDiscount && ol.ItemWeight == itemWeight);
		}

		public void Recalculate(IShippingCalculator shippingCalculator)
		{
			foreach (EfOrderLine orderLine in this.OrderLines)
			{
				orderLine.Recalculate();
			}

			this.LastUpdated = DateTime.UtcNow;
			this.ItemsCount = this.OrderLines.Sum(orderline => orderline.Quantity);
			this.Weight = this.OrderLines.Sum(orderline => orderline.Weight);
			this.OrderLinesPrice = this.OrderLines.Sum(orderline => orderline.Price);
			this.Shipping = shippingCalculator.GetShuppingCosts(Mapper.Map<Order>(this));
			this.Price = this.OrderLinesPrice + this.Shipping;
			this.LastUpdated = DateTime.UtcNow;
		}
	}
}
