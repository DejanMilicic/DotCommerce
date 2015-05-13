
namespace DotCommerce.Persistence.SqlServer.Test
{
	using System.Linq;

	using DotCommerce.Domain;
	using DotCommerce.Interfaces;
	using DotCommerce.Persistence.SqlServer.Test.Infrastructure;
	using DotCommerce.Persistence.SqlServer.Test.Infrastructure.DotCommerce;
	using DotCommerce.Persistence.SqlServer.Test.Infrastructure.DTO;
	using Respawn;
	using Should;

	public class OrderServiceTests
	{
		private IOrder order;
		private readonly IDotCommerceApi dc;

		public OrderServiceTests()
		{
			(new Checkpoint()).Reset(@"Server=.\SQLEXPRESS;Database=DotCommerce;Integrated Security=SSPI");
			
			dc = new DotCommerceApi(new ShippingCalculator());
			order = dc.GetOrCreateOrder("user123");
		}

		public void AddItemToCart(Product product)
		{
			order = dc.AddProductToOrder(order, product);

			IOrder saved = dc.GetOrCreateOrder(order.UserId);
			saved.OrderLines.Count().ShouldEqual(1);
			saved.OrderLines.Count().ShouldEqual(order.OrderLines.Count());
			saved.UserId.ShouldEqual(order.UserId);
			saved.Status.ShouldEqual(OrderStatus.Incomplete);
			saved.Status.ShouldEqual(order.Status);
			saved.ItemsCount.ShouldEqual(saved.OrderLines.Sum(x => x.Quantity));
			saved.ItemsCount.ShouldEqual(order.ItemsCount);
	
			saved.Weight.ShouldEqual(saved.OrderLines.Sum(x => x.Weight));
			saved.Weight.ShouldEqual(order.Weight);
			saved.OrderLinesPrice.ShouldEqual(saved.OrderLines.Sum(x => x.Price));
			saved.OrderLinesPrice.ShouldEqual(order.OrderLinesPrice);
			saved.Shipping.ShouldEqual(order.Shipping);
			saved.Price.ShouldEqual(saved.OrderLinesPrice + saved.Shipping);
			saved.Price.ShouldEqual(order.Price);

			IOrderLine savedOl = saved.OrderLines.First();
			IOrderLine ol = order.OrderLines.First();
			savedOl.ItemId.ShouldEqual(product.Id);
			savedOl.ItemId.ShouldEqual(ol.ItemId);
			savedOl.ItemName.ShouldEqual(product.Name);
			savedOl.ItemName.ShouldEqual(ol.ItemName);
			savedOl.ItemPrice.ShouldEqual(product.Price);
			savedOl.ItemPrice.ShouldEqual(ol.ItemPrice);
			savedOl.ItemDiscount.ShouldEqual(product.Discount);
			savedOl.ItemDiscount.ShouldEqual(ol.ItemDiscount);
			savedOl.ItemWeight.ShouldEqual(product.Weight);
			savedOl.ItemWeight.ShouldEqual(ol.ItemWeight);
			savedOl.ItemUrl.ShouldEqual(product.Url);
			savedOl.ItemUrl.ShouldEqual(ol.ItemUrl);
			savedOl.ItemImageUrl.ShouldEqual(product.ImageUrl);
			savedOl.ItemImageUrl.ShouldEqual(ol.ItemImageUrl);
			savedOl.Quantity.ShouldEqual(product.Quantity);
			savedOl.Quantity.ShouldEqual(ol.Quantity);
			savedOl.Weight.ShouldEqual(product.Weight * product.Quantity);
			savedOl.Weight.ShouldEqual(ol.Weight);
			savedOl.Price.ShouldEqual(product.Price * product.Quantity * ((100 - product.Discount) / (100)));
			savedOl.Price.ShouldEqual(ol.Price);
		}
	}
}