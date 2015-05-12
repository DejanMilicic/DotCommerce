
namespace DotCommerce.Infrastructure.EntityFramework
{
	using System.Data.Entity;

	class DbContextProvider : IDbContextProvider
	{
		public DbContext GetDbContext()
		{
			return new Db();
		}
	}
}
