
namespace DotCommerce.Domain
{
	using Headspring;

	public class SortingField : Enumeration<SortingField, string>
	{
		public static readonly SortingField Status = new SortingField("Status", "Status");
		public static readonly SortingField UserId = new SortingField("UserId", "UserId");
		public static readonly SortingField CreatedOn = new SortingField("CreatedOn", "CreatedOn");
		public static readonly SortingField LastUpdated = new SortingField("LastUpdated", "LastUpdated");
		public static readonly SortingField ItemsCount = new SortingField("ItemsCount", "ItemsCount");
		public static readonly SortingField Weight = new SortingField("Weight", "Weight");
		public static readonly SortingField OrderLinesPrice = new SortingField("OrderLinesPrice", "OrderLinesPrice");
		public static readonly SortingField Shipping = new SortingField("Shipping", "Shipping");
		public static readonly SortingField Price = new SortingField("Price", "Price");
		public static readonly SortingField Ordinal = new SortingField("Ordinal", "Ordinal");

		public SortingField(string value, string displayName)
			: base(value, displayName)
		{
		}
	}
}
