
namespace DotCommerce.Domain
{
	using Headspring;

	public class LogAction : Enumeration<LogAction, string>
	{
		public static readonly LogAction CreateOrder = new LogAction("CreateOrder", "CreateOrder");
		public static readonly LogAction AddItemToOrder = new LogAction("AddItemToOrder", "AddItemToOrder");
		public static readonly LogAction RemoveOrderLine = new LogAction("RemoveOrderLine", "RemoveOrderLine");
		public static readonly LogAction ChangeQuantity = new LogAction("ChangeQuantity", "ChangeQuantity");
		public static readonly LogAction SetShippingAddress = new LogAction("SetShippingAddress", "SetShippingAddress");
		public static readonly LogAction SetBillingAddress = new LogAction("SetBillingAddress", "SetBillingAddress");
		public static readonly LogAction SetOrderStatus = new LogAction("SetOrderStatus", "SetOrderStatus");
		public static readonly LogAction SetUser = new LogAction("SetUser", "SetUser");
		public static readonly LogAction SetOrdinal = new LogAction("SetOrdinal", "SetOrdinal");
		public static readonly LogAction SetNotes = new LogAction("SetNotes", "SetNotes");

		public LogAction(string value, string displayName) : base(value, displayName)
		{
		}
	}
}
