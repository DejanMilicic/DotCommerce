
namespace DotCommerce.Persistence.SqlServer.Test.Infrastructure.Support
{
	using global::DotCommerce.Interfaces;
	using global::DotCommerce.Persistence.SqlServer.Test.Infrastructure.DTO;

	using Respawn;

	public static class TestSupport
	{
		public static IOrder AddProductToOrder(this IDotCommerceApi dc, IOrder order, Product product)
		{
			return dc.AddItemToOrder(
				orderId: order.Id,
				itemid: product.Id,
				quantity: product.Quantity,
				price: product.Price,
				name: product.Name,
				discount: product.Discount,
				weight: product.Weight,
				url: product.Url,
				imageUrl: product.ImageUrl);
		}

		public static void ResetDatabase()
		{
			Checkpoint checkpoint = new Checkpoint
									{
										TablesToIgnore = new[]
										{
											"__MigrationHistory"
										},
									};

			checkpoint.Reset(@"Server=.\SQLEXPRESS;Database=DotCommerce;Integrated Security=SSPI");
		}
	}
}
