
namespace DotCommerce.Domain
{
	using Headspring;

	public class OrderStatus : Enumeration<OrderStatus, string>
	{
		public static readonly OrderStatus Incomplete = new OrderStatus("Incomplete", "Incomplete");
		public static readonly OrderStatus Closed = new OrderStatus("Closed", "Closed");


		/*
		Cancelled,
		PaymentFailed,
		Confirmed,
		OfflinePayment,
		Pending,
		ReadyForDispatch,
		ReadyForDispatchWhenStockArrives,
		Dispatched,
		Undeliverable,
		WaitingForPayment,
		WaitingForPaymentProvider,
		Returned,
		Wishlist
		*/

		public OrderStatus(string value, string displayName): base(value, displayName)
		{
		}
	}
}
