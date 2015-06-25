
namespace DotCommerce.Domain
{
	using Headspring;

	public class SortingDirection : Enumeration<SortingDirection, string>
	{
		public static readonly SortingDirection Ascending = new SortingDirection("ASC", "ASC");
		public static readonly SortingDirection Descending = new SortingDirection("DESC", "DESC");

		public SortingDirection(string value, string displayName)
			: base(value, displayName)
		{
		}

		public SortingDirection Reverse()
		{
			if (this == Ascending)
			{
				return Descending;
			}
			else
			{
				return Ascending;
			}
		}
	}
}
