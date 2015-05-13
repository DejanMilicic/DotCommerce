
namespace DotCommerce
{
	using System;
	using System.Data.Entity;
	using System.Linq;
	using AutoMapper;
	using DotCommerce.Domain;
	using DotCommerce.Infrastructure;
	using DotCommerce.Infrastructure.EntityFramework;
	using DotCommerce.Infrastructure.EntityFramework.Entities;
	using DotCommerce.Interfaces;

	public class DotCommerceApi : IDotCommerceApi
	{
		private readonly IShippingCalculator shippingCalculator;

		public DotCommerceApi(IShippingCalculator shippingCalculator)
		{
			AutomapperBootstrapper.Configure();
			this.shippingCalculator = shippingCalculator;
		}

		/// <summary>
		/// For user return order with status incomplete, or create new one otherwise
		/// </summary>
		public IOrder GetOrCreateOrder(string userId)
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = db.Orders.Where(x => x.UserId == userId).Include(x => x.OrderLines).FirstOrDefault();

				if (efOrder == null)
				{
					efOrder = new EfOrder(userId);
					db.Orders.Add(efOrder);
					db.SaveChanges();
				}

				return Mapper.Map<Order>(efOrder);
			}
		}

		public IOrder Get(int orderId)
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = db.Orders.Include(x => x.OrderLines).FirstOrDefault(x => x.Id == orderId);
				return efOrder == null ? null : Mapper.Map<Order>(efOrder);
			}
		}

		public IOrder AddItemToOrder(int orderId, string itemid, int quantity, decimal price, 
			string name = "", decimal discount = 0, int weight = 0, string url = "", string imageUrl = "")
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = db.Orders.Include(x => x.OrderLines).SingleOrDefault(x => x.Id == orderId);

				if (efOrder == null) throw new Exception("Order with orderId does not exist");

				EfOrderLine efOrderLine = efOrder.FindOrderLine(itemid, price, discount, weight);

				if (efOrderLine != null)
				{
					efOrderLine.Quantity += quantity;
				}
				else
				{
					efOrder.OrderLines.Add(new EfOrderLine(itemid, name, price, quantity, discount, weight, url, imageUrl));
				}

				efOrder.Recalculate(this.shippingCalculator);
				db.SaveChanges();

				return Mapper.Map<Order>(efOrder);
			}
		}
	}
}
