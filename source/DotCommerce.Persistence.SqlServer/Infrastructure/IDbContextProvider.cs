
namespace DotCommerce.Persistence.SqlServer.Infrastructure
{
	using System.Data.Entity;

	public interface IDbContextProvider
	{
		DbContext GetDbContext();
	}
}
