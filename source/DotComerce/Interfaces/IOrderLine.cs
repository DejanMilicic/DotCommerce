
namespace DotCommerce.Interfaces
{
	public interface IOrderLine
	{
		/// <summary>
		/// Id of the order
		/// </summary>
		int Id { get; }

		/// <summary>
		/// Id of item contained in the order line
		/// </summary>
		string ItemId { get; }

		/// <summary>
		/// Name of item contained in the order line
		/// </summary>
		string ItemName { get; }

		/// <summary>
		/// Order line discount in percentage, between 0 and 100
		/// </summary>
		decimal ItemDiscount { get; }

		/// <summary>
		/// Item weight (
		/// </summary>
		int ItemWeight { get; }

		/// <summary>
		/// URL of image for the item in this order line
		/// </summary>
		string ItemImageUrl { get; }

		/// <summary>
		/// URL pointing to the item
		/// </summary>
		string ItemUrl { get; }

		/// <summary>
		/// Price of a single item
		/// </summary>
		decimal ItemPrice { get; }

		int Weight { get; }

		/// <summary>
		/// Number of items in the cart
		/// </summary>
		int Quantity { get; }

		/// <summary>
		/// Order line price, discount taken into account
		/// </summary>
		decimal Price { get; }
	}
}