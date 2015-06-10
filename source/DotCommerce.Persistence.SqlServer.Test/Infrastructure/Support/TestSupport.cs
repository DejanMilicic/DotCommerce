
namespace DotCommerce.Persistence.SqlServer.Test.Infrastructure.Support
{
	using System.Collections.Generic;
	using System.Configuration;

	using global::DotCommerce.Domain;
	using global::DotCommerce.Interfaces;
	using global::DotCommerce.Persistence.SqlServer.Test.Infrastructure.DTO;

	using Respawn;

	using Shouldly;

	public static class TestSupport
	{
		public static void AddProductToOrder(this IDotCommerceApi dc, IOrder order, Product product)
		{
			dc.AddItemToOrder(
				order: order,
				itemid: product.Id,
				quantity: product.Quantity,
				price: product.Price,
				name: product.Name,
				discount: product.Discount,
				weight: product.Weight,
				url: product.Url,
				imageUrl: product.ImageUrl);
		}

		public static void SetShippingAddress(this IDotCommerceApi dc, IOrder order, Address address)
		{
			dc.SetShippingAddress(
				order: order, 
				title: address.Title, firstName: address.FirstName, lastName: address.LastName, company: address.Company,
				street: address.Street, streetNumber: address.StreetNumber, city: address.City, zip: address.Zip,
				country: address.Country, state: address.State, province: address.Province, email: address.Email,
				phone: address.Phone, singleAddress: address.SingleAddress);
		}

		public static void SetBillingAddress(this IDotCommerceApi dc, IOrder order, Address address)
		{
			dc.SetBillingAddress(
				order: order, 
				title: address.Title, firstName: address.FirstName, lastName: address.LastName, company: address.Company,
				street: address.Street, streetNumber: address.StreetNumber, city: address.City, zip: address.Zip,
				country: address.Country, state: address.State, province: address.Province, email: address.Email,
				phone: address.Phone, singleAddress: address.SingleAddress);
		}

		public static void ResetDatabase()
		{
			Checkpoint checkpoint = new Checkpoint
									{
										TablesToIgnore = new[]
										{
											"__MigrationHistory"
										},
									};

			try
			{
				checkpoint.Reset(ConfigurationManager.ConnectionStrings["DotCommerce"].ConnectionString);
			}
			catch
			{
				// we will get exception in he case when database is not existing
			}
		}

		public static void VerifyLogEntries(this IDotCommerceApi dc, IOrder order, List<LogAction> actions)
		{
			var log = dc.GetLogEntries(order.Id);
			log.Count.ShouldBe(actions.Count);
			for (int i = 0; i < actions.Count; i++)
			{
				log[i].Action.ShouldBe(actions[i]);
			}
		}
	}
}
