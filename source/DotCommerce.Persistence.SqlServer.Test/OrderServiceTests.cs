
namespace DotCommerce.Persistence.SqlServer.Test
{
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
			order = dc.GetOrCreateOrder("user123");
		}

		public void AddNewItemToOrder(Product product)
		{
			order = dc.AddProductToOrder(order, product);

			IOrder saved = dc.GetOrCreateOrder(order.UserId);
			saved.OrderLines.Count().ShouldBe(1);
			saved.OrderLines.Count().ShouldBe(order.OrderLines.Count());
			saved.UserId.ShouldBe(order.UserId);
			saved.Status.ShouldBe(OrderStatus.Incomplete);
			saved.Status.ShouldBe(order.Status);
			saved.ItemsCount.ShouldBe(saved.OrderLines.Sum(x => x.Quantity));
			saved.ItemsCount.ShouldBe(order.ItemsCount);

			saved.Weight.ShouldBe(saved.OrderLines.Sum(x => x.Weight));
			saved.Weight.ShouldBe(order.Weight);
			saved.OrderLinesPrice.ShouldBe(saved.OrderLines.Sum(x => x.Price));
			saved.OrderLinesPrice.ShouldBe(order.OrderLinesPrice);
			saved.Shipping.ShouldBe(order.Shipping);
			saved.Price.ShouldBe(saved.OrderLinesPrice + saved.Shipping);
			saved.Price.ShouldBe(order.Price);

			IOrderLine savedOl = saved.OrderLines.First();
			IOrderLine ol = order.OrderLines.First();
			savedOl.ItemId.ShouldBe(product.Id);
			savedOl.ItemId.ShouldBe(ol.ItemId);
			savedOl.ItemName.ShouldBe(product.Name);
			savedOl.ItemName.ShouldBe(ol.ItemName);
			savedOl.ItemPrice.ShouldBe(product.Price);
			savedOl.ItemPrice.ShouldBe(ol.ItemPrice);
			savedOl.ItemDiscount.ShouldBe(product.Discount);
			savedOl.ItemDiscount.ShouldBe(ol.ItemDiscount);
			savedOl.ItemWeight.ShouldBe(product.Weight);
			savedOl.ItemWeight.ShouldBe(ol.ItemWeight);
			savedOl.ItemUrl.ShouldBe(product.Url);
			savedOl.ItemUrl.ShouldBe(ol.ItemUrl);
			savedOl.ItemImageUrl.ShouldBe(product.ImageUrl);
			savedOl.ItemImageUrl.ShouldBe(ol.ItemImageUrl);
			savedOl.Quantity.ShouldBe(product.Quantity);
			savedOl.Quantity.ShouldBe(ol.Quantity);
			savedOl.Weight.ShouldBe(product.Weight * product.Quantity);
			savedOl.Weight.ShouldBe(ol.Weight);
			savedOl.Price.ShouldBe(product.Price * product.Quantity * ((100 - product.Discount) / (100)));
			savedOl.Price.ShouldBe(ol.Price);
		}

		public void AddExistingItemToOrder(Product product)
		{
			int secondQuantity = 3;
			int totalQuantity = product.Quantity + secondQuantity;

			dc.AddProductToOrder(order, product);
			product.Quantity = secondQuantity;
			order = dc.AddProductToOrder(order, product);

			order.OrderLines.Count().ShouldBe(1);
			order.ItemsCount.ShouldBe(totalQuantity);
		}

		public void RemoveOrderLine(Product product1, Product product2)
		{
			order = dc.AddProductToOrder(order, product1);
			int orderLineForRemoval = order.OrderLines.First().Id;

			order = dc.AddProductToOrder(order, product2);
			order.OrderLines.Count().ShouldBe(2);

			order = dc.RemoveOrderLine(orderLineForRemoval);
			order.OrderLines.Count().ShouldBe(1);
			order.OrderLines.First().ItemId.ShouldBe(product2.Id);
		}

		public void ChangeQuantity(Product product1)
		{
			product1.Quantity = 10;

			order = dc.AddProductToOrder(order, product1);
			order.ItemsCount.ShouldBe(10);

			order = dc.ChangeQuantity(order.OrderLines.First().Id, 3);
			order.ItemsCount.ShouldBe(3);
		}

		public void SetShippingAddress(Address shippingAddress)
		{
			order = dc.SetShippingAddress(order, shippingAddress);
			AreEqual(order.ShippingAddress, shippingAddress).ShouldBe(true);
		}

		public void SetBillingAddress(Address billingAddress)
		{
			order = dc.SetBillingAddress(order, billingAddress);
			AreEqual(order.BillingAddress, billingAddress).ShouldBe(true);
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