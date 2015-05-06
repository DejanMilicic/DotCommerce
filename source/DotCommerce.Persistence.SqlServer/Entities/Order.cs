
namespace DotCommerce.Persistence.SqlServer.Entities
{
	using System;
	using System.Collections.Generic;

	using DotCommerce.Domain;

	public class Order
	{
		public Order()
		{
			this.OrderLines = new List<OrderLine>();
		}

		public int Id { get; set; }

		public string UserId { get; set; }

		public List<OrderLine> OrderLines { get; set; }

		public string Status { get; set; }

		public DateTime CreatedOn { get; set; }
	}
}
