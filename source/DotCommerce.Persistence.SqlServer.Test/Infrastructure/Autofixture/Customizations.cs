
namespace DotCommerce.Persistence.SqlServer.Test.Infrastructure.Autofixture
{
	using Ploeh.AutoFixture;

	public class Customizations : CompositeCustomization
	{
		public Customizations(): base(
			new ProductCustomization()
			)
		{
		}
	}
}