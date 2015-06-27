
namespace DotCommerce.Interfaces
{
	public interface IShippingCalculator
	{
		decimal GetShippingCosts(IOrder order);
	}
}
