
namespace DotCommerce.Persistence.SqlServer.EntityFramework
{
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using DotCommerce.Persistence.SqlServer.Migrations;

	public class Db : DbContext
	{
		public Db(): base("DotCommerce")
		{
			// Turn off lazy loading for performance increase, use eager loading - .Include()
			this.Configuration.LazyLoadingEnabled = false;
			this.Configuration.ProxyCreationEnabled = false;

			Database.SetInitializer(new MigrateDatabaseToLatestVersion<Db, Configuration>());
			this.Database.Initialize(false);
		}

		public DbSet<Entities.Order> Orders { get; set; }
		public DbSet<Entities.OrderLine> OrderLines { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Entities.Order>()
				.HasMany(order => order.OrderLines)
				.WithRequired(orderline => orderline.Order);

			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			base.OnModelCreating(modelBuilder);
		}
	}
}
