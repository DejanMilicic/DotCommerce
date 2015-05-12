
namespace DotCommerce.Interfaces
{
	public interface IShippingCalculator
	{
		decimal GetShuppingCosts(IOrder order);
	}
}
