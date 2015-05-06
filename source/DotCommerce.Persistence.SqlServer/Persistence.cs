
namespace DotCommerce.Persistence.SqlServer
{
	using System.Collections.Generic;
	using System.Linq;

	using AutoMapper;
	using AutoMapper.QueryableExtensions;

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

		public List<Domain.Order> Get(string userId, OrderStatus orderStatus, int take)
		{
			using (Db db = new Db())
			{
				List<Order> entitiesList = db.Orders.Where(x => x.UserId == userId).Take(take).ToList();

				return Mapper.Map<List<Order>, List<Domain.Order>>(entitiesList);
			}
		}
	}
}
