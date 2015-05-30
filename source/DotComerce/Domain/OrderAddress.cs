
namespace DotCommerce.Domain
{
	using DotCommerce.Interfaces;

	class OrderAddress : IOrderAddress
	{
		public OrderAddress(string title, string firstName, string lastName, string company, string street, string streetNumber, string city, string zip, string country, string state, string province, string email, string phone, bool singleAddress)
		{
			this.Title = title;
			this.FirstName = firstName;
			this.LastName = lastName;
			this.Company = company;
			this.Street = street;
			this.StreetNumber = streetNumber;
			this.City = city;
			this.Zip = zip;
			this.Country = country;
			this.State = state;
			this.Province = province;
			this.Email = email;
			this.Phone = phone;
			this.SingleAddress = singleAddress;
		}

		public string Title { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Company { get; set; }
		public string Street { get; set; }
		public string StreetNumber { get; set; }
		public string City { get; set; }
		public string Zip { get; set; }
		public string Country { get; set; }
		public string State { get; set; }
		public string Province { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public bool SingleAddress { get; set; }
	}
}
