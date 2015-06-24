
namespace DotCommerce.Domain
{
	public class SortingCriteria
	{
		public SortingField Field { get; set; }
		public SortingDirection Direction { get; set; }

		public override string ToString()
		{
			return Field + " " + Direction;
		}
	}
}
