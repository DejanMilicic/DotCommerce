
namespace DotCommerce.Infrastructure.EntityFramework.Entities
{
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("DotCommerceOrderLine")]
	class EfOrderLine
	{
		public EfOrderLine()
		{
			
		}

		public EfOrderLine(string itemId, string itemName, decimal itemPrice, int quantity,
			decimal itemDiscount = 0, int itemWeight = 0, string itemUrl = "", string itemImageUrl = "")
		{
			this.Id = 0;
			this.ItemId = itemId;
			this.ItemName = itemName;
			this.ItemDiscount = itemDiscount;
			this.ItemWeight = itemWeight;
			this.ItemImageUrl = itemImageUrl;
			this.ItemUrl = itemUrl;
			this.ItemPrice = itemPrice;
			this.Quantity = quantity;

			this.Recalculate();
		}

		public int Id { get; set; }
		public string ItemId { get; set; }
		public string ItemName { get; set; }
		public decimal ItemPrice { get; set; }
		public decimal ItemDiscount { get; set; }
		public int ItemWeight { get; set; }
		public string ItemUrl { get; set; }
		public string ItemImageUrl { get; set; }

		public int Weight { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }

		// Navigation properties
		public int OrderId { get; set; }
		public EfOrder Order { get; set; }

		public void Recalculate()
		{
			this.Weight = this.ItemWeight * this.Quantity;
			this.Price = (this.ItemPrice * this.Quantity) * ((100 - this.ItemDiscount) / 100);
		}
	}
}
