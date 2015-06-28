
namespace DotCommerce.Domain
{
	public class UserOrdersSummary
	{
		public string UserId { get; set; }
		public int TotalOrdersCount { get; set; }
		public decimal TotalOrdersAmount { get; set; }
	}
}
