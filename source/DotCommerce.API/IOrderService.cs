namespace DotCommerce.API
{
	using DotCommerce.Domain;

	public interface IOrderService
	{
		Order GetOrCreateOrder(string userId);
	}
}