
namespace DotCommerce.Persistence.SqlServer.Test.Infrastructure.Autofixture
{
	using global::DotCommerce.Persistence.SqlServer.Test.Infrastructure.DTO;
	using Ploeh.AutoFixture;

	public class ProductCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customizations.Add(new DiscountBuilder());

		}
	}
}

		//public string Id { get; set; }
		//public string Name { get; set; }
		//public decimal Price { get; set; }
		//public decimal Discount { get; set; }
		//public int Weight { get; set; }
		//public string ImageUrl { get; set; }
		//public string Url { get; set; }