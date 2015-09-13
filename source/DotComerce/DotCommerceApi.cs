
namespace DotCommerce
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Linq.Dynamic;

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

		#region Private Methods

		private IQueryable<EfOrder> GetQueryableOrders(
			Db db, 
			OrderStatus orderStatus = null,
			string userId = null,
			Expression<Func<EfOrder, bool>> criteria = null)
		{
			var query = db.Orders
				.Include(x => x.OrderLines)
				.Include(x => x.BillingAddress)
				.Include(x => x.ShippingAddress)
				.Include(x => x.OrderLogs);

			if (orderStatus != null)
			{
				string orderStatusString = orderStatus.ToString();
				query = query.Where(x => x.Status == orderStatusString);
			}

			if (userId != null)
			{
				query = query.Where(x => x.UserId == userId);
			}

			if (criteria != null)
			{
				query = query.Where(criteria);
			}

			return query;
		}

		private EfOrder GetIncompleteOrderForUser(Db db, string userId)
		{
			return this.GetQueryableOrders(db, userId: userId, orderStatus: OrderStatus.Incomplete)
				.FirstOrDefault();
		}

		private EfOrder GetOrderById(Db db, Guid orderId)
		{
			return this.GetQueryableOrders(db, criteria: x => x.Id == orderId)
				.FirstOrDefault();
		}

		private EfOrderLine GetOrderlineById(Db db, int orderlineId)
		{
			EfOrderLine efOrderLine = db.OrderLines.SingleOrDefault(x => x.Id == orderlineId);

			if (efOrderLine == null) throw new Exception("Order line with orderlineId does not exist");

			return efOrderLine;
		}

		private void LogEvent(Db db, Guid orderId, int orderLineId, LogAction action, string oldValue, string value)
		{
			EfOrderLog logEntry = new EfOrderLog();
			logEntry.OrderId = orderId;
			logEntry.OrderLineId = orderLineId;
			logEntry.DateTime = DateTime.Now;
			logEntry.Action = action.ToString();
			logEntry.Value = value;
			logEntry.OldValue = oldValue;
			db.OrderLogs.Add(logEntry);
		}

		#endregion

		/// <summary>
		/// For user return order with status incomplete, or create new one otherwise
		/// </summary>
		public IOrder GetIncompleteOrder(string userId)
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = this.GetIncompleteOrderForUser(db, userId);
				return efOrder == null ? new Order(userId) : Mapper.Map<Order>(efOrder);
			}
		}

		public IOrder Get(Guid orderId)
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = GetOrderById(db, orderId);
				return efOrder == null ? null : Mapper.Map<Order>(efOrder);
			}
		}

		private EfOrder CreateNewOrder(Db db, IOrder order)
		{
			EfOrder newOrder = new EfOrder(order.UserId, order.Id);
			db.Orders.Add(newOrder);
			LogEvent(db, newOrder.Id, 0, LogAction.CreateOrder, "", "");
			return newOrder;
		}

		private EfOrder GetOrCreateById(Db db, IOrder order)
		{
			return this.GetOrderById(db, order.Id) ?? this.CreateNewOrder(db, order);
		}

		public void AddItemToOrder(IOrder order, string itemid, int quantity, decimal price, 
			string name = "", decimal discount = 0, int weight = 0, string url = "", string imageUrl = "")
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = GetOrCreateById(db, order);
				EfOrderLine efOrderLine = efOrder.FindOrderLine(itemid, price, discount, weight);

				if (efOrderLine != null)
				{
					efOrderLine.Quantity += quantity;
				}
				else
				{
					efOrderLine = new EfOrderLine(itemid, name, price, quantity, discount, weight, url, imageUrl);
					efOrder.OrderLines.Add(efOrderLine);
				}

				efOrder.Recalculate(this.shippingCalculator);

				db.SaveChanges();

				LogEvent(db, efOrder.Id, efOrderLine.Id, LogAction.AddItemToOrder, "",
					"itemid:" + itemid + ", quantity: " + quantity + ", price: " + price + ", name: " + name + ", discount: " + discount + ", weight: " + weight + ", url: " + url + ", imageUrl: " + imageUrl);

				db.SaveChanges();
			}
		}

		public void RemoveOrderLine(int orderLineId)
		{
			using (Db db = new Db())
			{
				EfOrderLine orderline = GetOrderlineById(db, orderLineId);
				if (orderline != null)
				{
					EfOrder order = GetOrderById(db, orderline.OrderId);
					if (order.Status == OrderStatus.Incomplete.ToString())
					{
						db.Entry(orderline).State = EntityState.Deleted;
						order.OrderLines.Remove(orderline);
						order.Recalculate(this.shippingCalculator);

						LogEvent(db, order.Id, orderLineId, LogAction.RemoveOrderLine, "", "");

						db.SaveChanges();
					}
				}
			}
		}

		public void ChangeQuantity(int orderLineId, int quantity)
		{
			if (quantity < 0) throw new ArgumentException("quantity is below zero");

			if (quantity == 0)
			{
				this.RemoveOrderLine(orderLineId);
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

						LogEvent(db, order.Id, orderLineId, LogAction.ChangeQuantity, oldQuantity.ToString(), quantity.ToString());

						db.SaveChanges();
					}
				}
			}
		}

		public void SetShippingAddress(IOrder order, 
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
				EfOrder efOrder = GetOrCreateById(db, order);
				string oldShippingAddress = order.ShippingAddress == null ? "" : order.ShippingAddress.ToString();
				EfAddress shippingAddress = new EfAddress(title, firstName, lastName, company, street, streetNumber, city, zip, country, state, province, email, phone, singleAddress);

				efOrder.ShippingAddress = shippingAddress;
				efOrder.Recalculate(this.shippingCalculator);

				LogEvent(db, efOrder.Id, 0, LogAction.SetShippingAddress, oldShippingAddress, shippingAddress.ToString());

				db.SaveChanges();
			}
		}

		public void SetBillingAddress(IOrder order, 
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
				EfOrder efOrder = GetOrCreateById(db, order);
				string oldBillingAddress = order.BillingAddress == null ? "" : order.BillingAddress.ToString();
				EfAddress billingAddress = new EfAddress(title, firstName, lastName, company, street, streetNumber, city, zip, country, state, province, email, phone, singleAddress);

				efOrder.BillingAddress = billingAddress;
				efOrder.Recalculate(this.shippingCalculator);

				LogEvent(db, efOrder.Id, 0, LogAction.SetBillingAddress, oldBillingAddress, billingAddress.ToString());

				db.SaveChanges();
			}
		}

		public void SetStatus(IOrder order, OrderStatus status)
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = GetOrCreateById(db, order);
				string existingStatus = efOrder.Status;

				efOrder.Status = status.ToString();

				LogEvent(db, efOrder.Id, 0, LogAction.SetOrderStatus, existingStatus, status.ToString());

				db.SaveChanges();
			}
		}

		public void SetUser(IOrder order, string userId)
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = GetOrCreateById(db, order);
				string existingUser = efOrder.UserId;
				efOrder.UserId = userId;

				LogEvent(db, efOrder.Id, 0, LogAction.SetUser, existingUser, userId);

				db.SaveChanges();
			}
		}

		public void SetNotes(IOrder order, string notes)
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = GetOrCreateById(db, order);
				string existingNotes = efOrder.Notes;
				efOrder.Notes = notes;

				LogEvent(db, efOrder.Id, 0, LogAction.SetNotes, existingNotes, notes);

				db.SaveChanges();
			}			
		}
		
		/// <summary>
		/// Get orders, paged, sorted by descending created date
		/// pageIndex is zero-based
		/// </summary>
		public List<IOrder> GetOrders(int pageIndex, int pageSize, out int totalCount,
			OrderStatus orderStatus = null,
			string userId = null,
			List<SortingCriteria> sortBy = null)
		{
			List<IOrder> orders = new List<IOrder>();

			using (Db db = new Db())
			{
				var query = this.GetQueryableOrders(db, orderStatus, userId);
				query = query.OrderByDescending(x => x.CreatedOn);

				// get total count before paging is applied
				totalCount = query.Count();

				// sorting
				if (sortBy != null && sortBy.Any())
				{
					query = query.OrderBy(String.Join(", ", sortBy.Select(x => x.ToString())));
				}
				
				query = query.Skip(pageIndex * pageSize).Take(pageSize);
				
				// read-only operation, no need to track changes
				query = query.AsNoTracking();

				orders.AddRange(query.ToList().Select(Mapper.Map<Order>));
			}

			return orders;
		}

		public List<IOrderLog> GetLogEntries(Guid orderId)
		{
			using (Db db = new Db())
			{
				List<IOrderLog> orderLogs = new List<IOrderLog>();

				var orderLogsQuery = db.OrderLogs.Where(x => x.OrderId == orderId);
				
				// read-only operation, no need to track changes
				orderLogsQuery = orderLogsQuery.AsNoTracking();

				orderLogs.AddRange(Mapper.Map<List<OrderLog>>(orderLogsQuery));

				return orderLogs;
			}
		}

		public void AssignOrdinal(IOrder order)
		{
			using (Db db = new Db())
			{
				int ordinal = 1;

				EfOrder efOrder = GetOrCreateById(db, order);

				int? largestOrdinal = db.Orders.Max(x => x.Ordinal);
				if (largestOrdinal.HasValue)
				{
					ordinal = largestOrdinal.Value + 1;
				}

				efOrder = this.GetOrderById(db, order.Id);
				efOrder.Ordinal = ordinal;
				LogEvent(db, efOrder.Id, 0, LogAction.SetOrdinal, "", ordinal.ToString());

				db.SaveChanges();
			}
		}

		public List<UserOrdersSummary> GetUserOrdersSummary(int pageIndex, int pageSize, out int totalCount)
		{
			using (Db db = new Db())
			{
				List<UserOrdersSummary> summaries = new List<UserOrdersSummary>();

				string incompleteStatus = OrderStatus.Incomplete.ToString();

				var groupedQuery = this.GetQueryableOrders(db)
					.Where(x => x.Status != incompleteStatus)
					.GroupBy(x => x.UserId)
					.OrderByDescending(group => group.Count());

				// get total count before paging is applied
				totalCount = groupedQuery.Count();

				summaries.AddRange(
					groupedQuery
					.Skip(pageIndex * pageSize).Take(pageSize)
					.Select(group => new UserOrdersSummary
								 {
									 UserId = group.Key,
									 TotalOrdersCount = group.Count(),
									 TotalOrdersAmount = group.Sum(g => g.Price)
								 })
				);

				return summaries;
			}
		}

		public void SetShipping(IOrder order, bool isShipping)
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = GetOrCreateById(db, order);

				bool oldIsShipping = efOrder.IsShipping;
				efOrder.IsShipping = isShipping;

				efOrder.Recalculate(this.shippingCalculator);

				db.SaveChanges();

				LogEvent(db, efOrder.Id, 0, LogAction.SetShipping, oldIsShipping.ToString(), isShipping.ToString());

				db.SaveChanges();
			}
		}

		public void SetShippingDate(IOrder order, DateTime? shippingDate)
		{
			using (Db db = new Db())
			{
				EfOrder efOrder = GetOrCreateById(db, order);

				DateTime? oldShippingDate = efOrder.ShippingDate;
				efOrder.ShippingDate = shippingDate;

				db.SaveChanges();

				LogEvent(db, efOrder.Id, 0, LogAction.SetShippingDate, oldShippingDate.ToString(), shippingDate.ToString());

				db.SaveChanges();
			}
		}
	}
}
