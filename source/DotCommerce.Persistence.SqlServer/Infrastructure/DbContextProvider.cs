
namespace DotCommerce.Persistence.SqlServer.Infrastructure
{
	using System.Data.Entity;
	using DotCommerce.Persistence.SqlServer.EntityFramework;

	public class DbContextProvider : IDbContextProvider
	{
		public DbContext GetDbContext()
		{
			return new Db();
		}
	}
}
