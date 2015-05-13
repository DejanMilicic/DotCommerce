
using System.Reflection;

namespace DotCommerce.Persistence.SqlServer.Test.Infrastructure.Autofixture
{
	using Ploeh.AutoFixture.Kernel;

	public class DiscountBuilder : ISpecimenBuilder
	{
		public object Create(object request, ISpecimenContext context)
		{
			var pi = request as PropertyInfo;
			if (pi == null ||
				pi.Name != "Discount" ||
				pi.PropertyType != typeof(decimal))
				return new NoSpecimen(request);

			return context.Resolve(
				new RangedNumberRequest(typeof(decimal), 0.0m, 100.0m));
		}
	}
}
