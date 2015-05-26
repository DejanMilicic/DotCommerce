
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

		private EfOrder GetOrderByUser(Db db, string userId)
		{
			return db.Orders
				.Include(x => x.OrderLines)
				.Include(x => x.BillingAddress)
				.Include(x => x.ShippingAddress)
				.FirstOrDefault(x => x.UserId == userId);
		}

		private EfOrder GetOrderById(Db db, int orderId)
		{
			EfOrder efOrder = db.Orders
				.Include(x => x.OrderLines)
				.Include(x => x.BillingAddress)
				.Include(x => x.ShippingAddress)
				.FirstOrDefault(x => x.Id == orderId);

			if (efOrder == null) throw new Exception("Order with orderId does not exist");

			return efOrder;
		}

		private EfOrderLine GetOrderlineById(Db db, int orderlineId)
		{
			EfOrderLine efOrderLine = db.OrderLines.SingleOrDefault(x => x.Id == orderlineId);

			if (efOrderLine == null) throw new Exception("Order line with orderlineId does not exist");

			return efOrderLine;
		}

		private void LogEvent(Db db, int orderId, LogAction action, string oldValue, string value)
		{
			EfOrderLog logEntry = new EfOrderLog();
			logEntry.OrderId = orderId;
			logEntry.DateTime = DateTime.Now;
			logEntry.Action = action.ToString();
			logEntry.Value = value;
			logEntry.OldValue = oldValue;
			db.OrderLogs.Add(logEntry);
			db.SaveChanges();			
		}

		/// <summary>
		/// For user return order with status incomplete, or create new one otherwise
		/// </summary>
		public IOrder GetOrCreateOrder(string userId)
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = GetOrderByUser(db, userId);

				if (efOrder == null)
				{
					efOrder = new EfOrder(userId);
					db.Orders.Add(efOrder);
					db.SaveChanges();

					LogEvent(db, efOrder.Id, LogAction.CreateOrder, "", "");
				}

				return Mapper.Map<Order>(efOrder);
			}
		}

		public IOrder Get(int orderId)
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = GetOrderById(db, orderId);
				return efOrder == null ? null : Mapper.Map<Order>(efOrder);
			}
		}

		public IOrder AddItemToOrder(int orderId, string itemid, int quantity, decimal price, 
			string name = "", decimal discount = 0, int weight = 0, string url = "", string imageUrl = "")
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = GetOrderById(db, orderId);
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

				LogEvent(db, orderId, LogAction.AddItemToOrder, "",
					"itemid:" + itemid + ", quantity: " + quantity + ", price: " + price + ", name: " + name + ", discount: " + discount + ", weight: " + weight + ", url: " + url + ", imageUrl: " + imageUrl);

				return Mapper.Map<Order>(efOrder);
			}
		}

		public IOrder RemoveOrderLine(int orderLineId)
		{
			using (Db db = new Db())
			{
				EfOrderLine orderline = GetOrderlineById(db, orderLineId);
				if (orderline == null)
				{
					return null;
				}
				else
				{
					EfOrder order = GetOrderById(db, orderline.OrderId);
					if (order.Status == OrderStatus.Incomplete.ToString())
					{
						db.Entry(orderline).State = EntityState.Deleted;
						order.OrderLines.Remove(orderline);
						order.Recalculate(this.shippingCalculator);
						db.SaveChanges();

						LogEvent(db, order.Id, LogAction.RemoveOrderLine, "", orderLineId.ToString());
					}

					return Mapper.Map<Order>(order);
				}
			}
		}

		public IOrder ChangeQuantity(int orderLineId, int quantity)
		{
			if (quantity < 0) throw new ArgumentException("quantity is below zero");

			if (quantity == 0)
			{
				return this.RemoveOrderLine(orderLineId);
			}
			else
			{
				using (Db db = new Db())
				{
					EfOrderLine orderline = GetOrderlineById(db, orderLineId);
					EfOrder order = this.GetOrderById(db, orderline.OrderId);
					if (order.Status == OrderStatus.Incomplete.ToString())
					{
						int oldQuantity = orderline.Quantity;
						orderline.Quantity = quantity;
						order.Recalculate(this.shippingCalculator);
						db.SaveChanges();

						LogEvent(db, order.Id, LogAction.ChangeQuantity, oldQuantity.ToString(), quantity.ToString());
					}

					return Mapper.Map<Order>(order);
				}
			}
		}

		public IOrder SetShippingAddress(int orderId, 
			string title = "",
			string firstName = "",
			string lastName = "",
			string company = "",
			string street = "",
			string streetNumber = "",
			string city = "",
			string zip = "",
			string country = "",
			string state = "",
			string province = "",
			string email = "",
			string phone = "", 
			bool singleAddress =  false
			)
		{
			using (Db db = new Db())
			{
				EfOrder order = GetOrderById(db, orderId);
				string oldShippingAddress = order.ShippingAddress == null ? "" : order.ShippingAddress.ToString();
				EfAddress shippingAddress = new EfAddress(title, firstName, lastName, company, street, streetNumber, city, zip, country, state, province, email, phone, singleAddress);

				order.ShippingAddress = shippingAddress;
				order.Recalculate(this.shippingCalculator);
				db.SaveChanges();

				LogEvent(db, order.Id, LogAction.SetShippingAddress, oldShippingAddress, shippingAddress.ToString());

				return Mapper.Map<Order>(order);
			}
		}

		public IOrder SetBillingAddress(int orderId, 
			string title = "",
			string firstName = "",
			string lastName = "",
			string company = "",
			string street = "",
			string streetNumber = "",
			string city = "",
			string zip = "",
			string country = "",
			string state = "",
			string province = "",
			string email = "",
			string phone = "", 
			bool singleAddress =  false
			)
		{
			using (Db db = new Db())
			{
				EfOrder order = GetOrderById(db, orderId);
				string oldBillingAddress = order.BillingAddress == null ? "" : order.BillingAddress.ToString();
				EfAddress billingAddress = new EfAddress(title, firstName, lastName, company, street, streetNumber, city, zip, country, state, province, email, phone, singleAddress);
				
				order.BillingAddress = billingAddress;
				order.Recalculate(this.shippingCalculator);
				db.SaveChanges();

				LogEvent(db, order.Id, LogAction.SetBillingAddress, oldBillingAddress, billingAddress.ToString());


				return Mapper.Map<Order>(order);
			}
		}

		public IOrder SetStatus(int orderId, OrderStatus status)
		{
			using (Db db = new Db())
			{
				EfOrder order = GetOrderById(db, orderId);
				string existingStatus = order.Status;

				order.Status = status.ToString();
				db.SaveChanges();

				LogEvent(db, order.Id, LogAction.SetOrderStatus, existingStatus, status.ToString());

				return Mapper.Map<Order>(order);
			}
		}

		public IOrder SetUser(int orderId, string userId)
		{
			using (Db db = new Db())
			{
				EfOrder order = GetOrderById(db, orderId);
				string existingUser = order.UserId;
				order.UserId = userId;
				db.SaveChanges();

				LogEvent(db, order.Id, LogAction.SetUser, existingUser, userId);

				return Mapper.Map<Order>(order);
			}
		}
	}
}
