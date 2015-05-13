﻿
namespace DotCommerce.Domain
{
	using System;
	using System.Collections.Generic;
	using DotCommerce.Interfaces;

	class Order : IOrder
	{
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
	}
}