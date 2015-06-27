
namespace DotCommerce.Persistence.SqlServer.Test.Infrastructure.DotCommerce
{
	using global::DotCommerce.Interfaces;

	public class ShippingCalculator : IShippingCalculator
	{
		public decimal GetShippingCosts(IOrder order)
		{
			return (decimal)10.00;
		}
	}
}