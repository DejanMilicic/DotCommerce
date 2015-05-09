
namespace DotCommerce.Domain
{
	public class OrderLine
	{
		public OrderLine()
		{

		}

		public OrderLine(int id, string itemId, string itemName, decimal price, int quantity, 
			decimal discount, int itemWeight, string imageUrl, string itemUrl)
		{
			this.Id = id;
			this.ItemId = itemId;
			this.ItemName = itemName;
			this.Discount = discount;
			this.ItemWeight = itemWeight;
			this.Quantity = quantity;
			this.ImageUrl = imageUrl;
			this.ItemUrl = itemUrl;
			this.ItemPrice = price;
		}

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

		/// <summary>
		/// Order line discount in percentage, between 0 and 100
		/// </summary>
		public decimal Discount { get; private set; }

		/// <summary>
		/// Item weight (
		/// </summary>
		public int ItemWeight { get; private set; }

		public int Weight
		{
			get
			{
				return this.Quantity * this.ItemWeight;
			}
		}

		/// <summary>
		/// Number of items in the cart
		/// </summary>
		public int Quantity { get; private set; }

		/// <summary>
		/// URL of image for the item in this order line
		/// </summary>
		public string ImageUrl { get; private set; }

		/// <summary>
		/// URL pointing to the item
		/// </summary>
		public string ItemUrl { get; private set; }

		/// <summary>
		/// Price of a single item
		/// </summary>
		public decimal ItemPrice { get; private set; }

		public decimal Price
		{
			get
			{
				return (this.ItemPrice * this.Quantity) * ((100 - this.Discount) / 100);
			}
		}
	}
}
