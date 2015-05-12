
namespace DotCommerce.Infrastructure.EntityFramework
{
	using System.Data.Entity;

	interface IDbContextProvider
	{
		DbContext GetDbContext();
	}
}
