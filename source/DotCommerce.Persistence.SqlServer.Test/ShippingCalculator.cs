
namespace DotCommerce.Persistence.SqlServer.Test
{
	using DotCommerce.Interfaces;

	public class ShippingCalculator : IShippingCalculator
	{
		public decimal GetShuppingCosts(IOrder order)
		{
			return (decimal)10.00;
		}
	}
}