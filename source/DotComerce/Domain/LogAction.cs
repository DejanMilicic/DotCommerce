
namespace DotCommerce.Domain
{
	using Headspring;

	public class LogAction : Enumeration<LogAction, string>
	{
		public static readonly LogAction CreateOrder = new LogAction("CreateOrder", "CreateOrder");
		public static readonly LogAction AddItemToOrder = new LogAction("AddItemToOrder", "AddItemToOrder");

		public LogAction(string value, string displayName)
			: base(value, displayName)
		{
		}
	}
}
