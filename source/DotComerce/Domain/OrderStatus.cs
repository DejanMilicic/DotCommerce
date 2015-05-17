
namespace DotCommerce.Domain
{
	using Headspring;

	public class OrderStatus : Enumeration<OrderStatus, string>
	{
		public static readonly OrderStatus Incomplete = new OrderStatus("Incomplete", "Incomplete");
		public static readonly OrderStatus Closed = new OrderStatus("Closed", "Closed");
		public static readonly OrderStatus Cancelled = new OrderStatus("Cancelled", "Cancelled");
		public static readonly OrderStatus PaymentFailed = new OrderStatus("PaymentFailed", "PaymentFailed");
		public static readonly OrderStatus Confirmed = new OrderStatus("Confirmed", "Confirmed");
		public static readonly OrderStatus OfflinePayment = new OrderStatus("OfflinePayment", "OfflinePayment");
		public static readonly OrderStatus Pending = new OrderStatus("Pending", "Pending");
		public static readonly OrderStatus ReadyForDispatch = new OrderStatus("ReadyForDispatch", "ReadyForDispatch");
		public static readonly OrderStatus ReadyForDispatchWhenStockArrives = new OrderStatus("ReadyForDispatchWhenStockArrives", "ReadyForDispatchWhenStockArrives");
		public static readonly OrderStatus Dispatched = new OrderStatus("Dispatched", "Dispatched");
		public static readonly OrderStatus Undeliverable = new OrderStatus("Undeliverable", "Undeliverable");
		public static readonly OrderStatus WaitingForPayment = new OrderStatus("WaitingForPayment", "WaitingForPayment");
		public static readonly OrderStatus WaitingForPaymentProvider = new OrderStatus("WaitingForPaymentProvider", "WaitingForPaymentProvider");
		public static readonly OrderStatus Returned = new OrderStatus("Returned", "Returned");
		public static readonly OrderStatus Wishlist = new OrderStatus("Wishlist", "Wishlist");

		public OrderStatus(string value, string displayName): base(value, displayName)
		{
		}
	}
}
