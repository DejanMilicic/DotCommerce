
namespace DotCommerce.Persistence.SqlServer
{
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using AutoMapper;
	using DotCommerce.Domain;
	using DotCommerce.Persistence.SqlServer.EntityFramework;
	using DotCommerce.Persistence.SqlServer.Infrastructure;

	using Order = DotCommerce.Persistence.SqlServer.Entities.Order;

	public class Persistence : IPersistence
	{
		public Persistence()
		{
			AutomapperBootstrapper.Configure();
		}

		public void Save(Domain.Order order)
		{
			using (Db db = new Db())
			{
				Entities.Order dbOrder = Mapper.Map<Entities.Order>(order);
				db.Orders.Add(dbOrder);
				db.SaveChanges();
			}
		}

		public void Save(int orderId, OrderLine orderLine)
		{
			using (Db db = new Db())
			{
				if (orderLine.Id == 0)
				{
					// insert new
					Entities.Order order = db.Orders.Single(x => x.Id == orderId);
					Entities.OrderLine newOrderLine = Mapper.Map<Domain.OrderLine, Entities.OrderLine>(orderLine);
					order.OrderLines.Add(newOrderLine);
				}
				else
				{
					// update existing
					var existingOrderLine = db.OrderLines.Single(x => x.Id == orderLine.Id);
					existingOrderLine.Quantity = orderLine.Quantity;
				}

				db.SaveChanges();
			}
		}

		public List<Domain.Order> Get(string userId, OrderStatus orderStatus, int take)
		{
			using (Db db = new Db())
			{
				List<Order> entitiesList = db.Orders.Where(x => x.UserId == userId).Include(x => x.OrderLines).Take(take).ToList();
				return Mapper.Map<List<Entities.Order>, List<Domain.Order>>(entitiesList);
			}
		}

		public Domain.Order Get(int orderId)
		{
			using (Db db = new Db())
			{
				Order order = db.Orders.Where(x => x.Id == orderId).Include(x => x.OrderLines).FirstOrDefault();
				return Mapper.Map<Entities.Order, Domain.Order>(order);
			}
		}
	}
}
