
namespace DotCommerce.Persistence.SqlServer.Test
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DotCommerce.Domain;
	using DotCommerce.Interfaces;
	using DotCommerce.Persistence.SqlServer.Test.Infrastructure.DotCommerce;
	using DotCommerce.Persistence.SqlServer.Test.Infrastructure.DTO;
	using DotCommerce.Persistence.SqlServer.Test.Infrastructure.Support;
	using Shouldly;

	public class OrderServiceTests
	{
		private IOrder order;
		private readonly IDotCommerceApi dc;

		public OrderServiceTests()
		{
			TestSupport.ResetDatabase();
			
			dc = new DotCommerceApi(new ShippingCalculator());
			order = dc.GetIncompleteOrder("user123");
		}

		public void AddNewItemToOrder(Product product)
		{
			dc.AddProductToOrder(order, product);
			order = dc.GetIncompleteOrder(order.UserId);

			order.OrderLines.Count().ShouldBe(1);
			order.UserId.ShouldBe(order.UserId);
			order.Status.ShouldBe(OrderStatus.Incomplete);
			order.ItemsCount.ShouldBe(order.OrderLines.Sum(x => x.Quantity));

			order.Weight.ShouldBe(order.OrderLines.Sum(x => x.Weight));
			order.OrderLinesPrice.ShouldBe(order.OrderLines.Sum(x => x.Price));
			order.Price.ShouldBe(order.OrderLinesPrice + order.Shipping);

			IOrderLine orderLine = order.OrderLines.First();
			orderLine.ItemId.ShouldBe(product.Id);
			orderLine.ItemName.ShouldBe(product.Name);
			orderLine.ItemPrice.ShouldBe(product.Price);
			orderLine.ItemDiscount.ShouldBe(product.Discount);
			orderLine.ItemWeight.ShouldBe(product.Weight);
			orderLine.ItemUrl.ShouldBe(product.Url);
			orderLine.ItemImageUrl.ShouldBe(product.ImageUrl);
			orderLine.Quantity.ShouldBe(product.Quantity);
			orderLine.Weight.ShouldBe(product.Weight * product.Quantity);
			orderLine.Price.ShouldBe(product.Price * product.Quantity * ((100 - product.Discount) / (100)));

			dc.VerifyLogEntries(order, new List<LogAction>
			                           {
				                           LogAction.CreateOrder,
										   LogAction.AddItemToOrder
			                           });
		}

		public void AddExistingItemToOrder(Product product)
		{
			int secondQuantity = 3;
			int totalQuantity = product.Quantity + secondQuantity;

			dc.AddProductToOrder(order, product);
			product.Quantity = secondQuantity;

			dc.AddProductToOrder(order, product);
			order = dc.GetIncompleteOrder(order.UserId);

			order.OrderLines.Count().ShouldBe(1);
			order.ItemsCount.ShouldBe(totalQuantity);

			dc.VerifyLogEntries(order, new List<LogAction>
			                           {
				                           LogAction.CreateOrder,
										   LogAction.AddItemToOrder,
										   LogAction.AddItemToOrder
			                           });
		}

		public void RemoveOrderLine(Product product1, Product product2)
		{
			dc.AddProductToOrder(order, product1);
			order = dc.Get(order.Id);
			int orderLineForRemoval = order.OrderLines.First().Id;

			dc.AddProductToOrder(order, product2);
			order = dc.Get(order.Id);
			order.OrderLines.Count().ShouldBe(2);

			dc.RemoveOrderLine(orderLineForRemoval);
			order = dc.GetIncompleteOrder(order.UserId);
			order.OrderLines.Count().ShouldBe(1);
			order.OrderLines.First().ItemId.ShouldBe(product2.Id);

			dc.VerifyLogEntries(order, new List<LogAction>
			                           {
				                           LogAction.CreateOrder,
										   LogAction.AddItemToOrder,
										   LogAction.AddItemToOrder,
										   LogAction.RemoveOrderLine
			                           });
		}

		public void ChangeQuantity(Product product1)
		{
			product1.Quantity = 10;

			dc.AddProductToOrder(order, product1);
			order = dc.Get(order.Id);
			order.ItemsCount.ShouldBe(10);

			dc.ChangeQuantity(order.OrderLines.First().Id, 3);
			order = dc.GetIncompleteOrder(order.UserId);
			order.ItemsCount.ShouldBe(3);

			dc.VerifyLogEntries(order, new List<LogAction>
			                           {
				                           LogAction.CreateOrder,
										   LogAction.AddItemToOrder,
										   LogAction.ChangeQuantity
			                           });
		}

		public void SetShippingAddress(Address shippingAddress)
		{
			dc.SetShippingAddress(order, shippingAddress);

			order = dc.GetIncompleteOrder(order.UserId);
			AreEqual(order.ShippingAddress, shippingAddress).ShouldBe(true);

			dc.VerifyLogEntries(order, new List<LogAction>
			                           {
				                           LogAction.CreateOrder,
										   LogAction.SetShippingAddress
			                           });
		}

		public void SetBillingAddress(Address billingAddress)
		{
			dc.SetBillingAddress(order, billingAddress);

			order = dc.GetIncompleteOrder(order.UserId);
			AreEqual(order.BillingAddress, billingAddress).ShouldBe(true);

			dc.VerifyLogEntries(order, new List<LogAction>
			                           {
				                           LogAction.CreateOrder,
										   LogAction.SetBillingAddress
			                           });
		}

		public void SetStatus()
		{
			order.Status.ShouldBe(OrderStatus.Incomplete);
			dc.SetStatus(order, OrderStatus.Closed);

			order = dc.Get(order.Id);
			order.Status.ShouldBe(OrderStatus.Closed);

			dc.VerifyLogEntries(order, new List<LogAction>
			                           {
				                           LogAction.CreateOrder,
										   LogAction.SetOrderStatus
			                           });
		}

		public void SetUser(string userId)
		{
			dc.SetUser(order, userId);

			order = dc.GetIncompleteOrder(userId);
			order.UserId.ShouldBe(userId);

			dc.VerifyLogEntries(order, new List<LogAction>
			                           {
				                           LogAction.CreateOrder,
										   LogAction.SetUser
			                           });
		}

		public void GetOrders(string user1, string user2, Product product1, Product product2)
		{
			// prepare test data
			int user1TotalOrders = 6;
			int user2TotalOrders = 9;
			int totalOrders = user1TotalOrders + user2TotalOrders;
			int pendingOrders = 3;
			int readyForDispatchOrders = 2;
			int closedOrders = totalOrders - pendingOrders - readyForDispatchOrders;

			List<Guid> allOrders = new List<Guid>();

			for (int i = 0; i < user1TotalOrders; i++)
			{
				order = dc.GetIncompleteOrder(user1);
				dc.AddProductToOrder(order, product1);
				dc.SetStatus(order, OrderStatus.Closed);
				allOrders.Add(order.Id);
			}

			for (int i = 0; i < user2TotalOrders; i++)
			{
				order = dc.GetIncompleteOrder(user2);
				dc.AddProductToOrder(order, product2);
				dc.SetStatus(order, OrderStatus.Closed);
				allOrders.Add(order.Id);
			}

			for (int i = 0; i < pendingOrders; i++)
			{
				order = dc.Get(allOrders[i]);
				dc.SetStatus(order, OrderStatus.Pending);
			}

			for (int i = pendingOrders; i < pendingOrders + readyForDispatchOrders; i++)
			{
				order = dc.Get(allOrders[i]);
				dc.SetStatus(order, OrderStatus.ReadyForDispatch);
			}

			int totalCount;
			// verify counts
			dc.GetOrders(0, 100, out totalCount).Count.ShouldBe(totalOrders);
			totalCount.ShouldBe(totalOrders);

			dc.GetOrders(0, 100, out totalCount, userId: user1).Count.ShouldBe(user1TotalOrders);
			totalCount.ShouldBe(user1TotalOrders);

			dc.GetOrders(0, 100, out totalCount, userId: user2).Count.ShouldBe(user2TotalOrders);
			totalCount.ShouldBe(user2TotalOrders);

			dc.GetOrders(0, 100, out totalCount, orderStatus: OrderStatus.Closed).Count.ShouldBe(closedOrders);
			totalCount.ShouldBe(closedOrders);

			dc.GetOrders(0, 3, out totalCount, orderStatus: OrderStatus.Closed).Count.ShouldBe(3);
			totalCount.ShouldBe(closedOrders);

			dc.GetOrders(1, 9, out totalCount, orderStatus: OrderStatus.Closed).Count.ShouldBe(1);
			totalCount.ShouldBe(closedOrders);

			dc.GetOrders(0, 100, out totalCount, orderStatus: OrderStatus.Pending).Count.ShouldBe(pendingOrders);
			totalCount.ShouldBe(pendingOrders);

			dc.GetOrders(0, 100, out totalCount, orderStatus: OrderStatus.ReadyForDispatch).Count.ShouldBe(readyForDispatchOrders);
			totalCount.ShouldBe(readyForDispatchOrders);

			// verify sorting
			List<IOrder> res = dc.GetOrders(0, 100, out totalCount, sortBy: new List<SortingCriteria>{new SortingCriteria{ Field = SortingField.Weight, Direction = SortingDirection.Ascending}});
			List<IOrder> sortedRes = res.OrderBy(x => x.Weight).ToList();
			res.ShouldBe(sortedRes);

			res = dc.GetOrders(0, 100, out totalCount, sortBy: new List<SortingCriteria>{new SortingCriteria{ Field = SortingField.ItemsCount, Direction = SortingDirection.Descending}});
			sortedRes = res.OrderByDescending(x => x.ItemsCount).ToList();
			res.ShouldBe(sortedRes);
		}

		public void AssignOrdinal()
		{
			dc.SetStatus(order, OrderStatus.Closed);
			dc.AssignOrdinal(order);

			order = dc.Get(order.Id);
			order.Ordinal.ShouldBe(1);

			IOrder order2 = dc.GetIncompleteOrder(order.UserId);
			dc.SetStatus(order2, OrderStatus.Closed);
			dc.AssignOrdinal(order2);

			order2 = dc.Get(order2.Id);
			order2.Ordinal.ShouldBe(2);
		}

		public void SetNotes(string notes)
		{
			dc.SetStatus(order, OrderStatus.Closed);
			dc.SetNotes(order, notes);

			order = dc.Get(order.Id);
			order.Notes.ShouldBe(notes);
		}

		private bool AreEqual(IOrderAddress orderAddress, Address address)
		{
			return orderAddress.Title == address.Title
				&& orderAddress.FirstName == address.FirstName
				&& orderAddress.LastName == address.LastName
				&& orderAddress.Company == address.Company
				&& orderAddress.Street == address.Street
				&& orderAddress.StreetNumber == address.StreetNumber
				&& orderAddress.City == address.City
				&& orderAddress.Zip == address.Zip
				&& orderAddress.Country == address.Country
				&& orderAddress.State == address.State
				&& orderAddress.Province == address.Province
				&& orderAddress.Email == address.Email
				&& orderAddress.Phone == address.Phone
				&& orderAddress.SingleAddress == address.SingleAddress;
		}
	}
}