
namespace DotCommerce.Domain
{
	public class OrderLine
	{
		/// <summary>
		/// Internal Id of the order
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		/// Id of item contained in the order line
		/// </summary>
		public string ItemId { get; private set; }

		/// <summary>
		/// Name of item contained in the order line
		/// </summary>
		public string ItemName { get; private set; }


	}
}
