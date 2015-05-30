
namespace DotCommerce.Interfaces
{
	public interface IOrderAddress
	{
		string Title { get;  }
		string FirstName { get; }
		string LastName { get; }
		string Company { get; }
		string Street { get; }
		string StreetNumber { get; }
		string City { get; }
		string Zip { get; }
		string Country { get; set; }
		string State { get; set; }
		string Province { get; set; }
		string Email { get; }
		string Phone { get; }
		bool SingleAddress { get; }
	}
}