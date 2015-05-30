
namespace DotCommerce.Infrastructure.EntityFramework
{
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;
	using DotCommerce.Infrastructure.EntityFramework.Entities;

	class Db : DbContext
	{
		public Db(): base("DotCommerce")
		{
			// Turn off lazy loading for performance increase, use eager loading - .Include()
			this.Configuration.LazyLoadingEnabled = false;
			this.Configuration.ProxyCreationEnabled = false;

//			Database.SetInitializer(new MigrateDatabaseToLatestVersion<Db, Configuration>());
			this.Database.Initialize(false);
		}

		public DbSet<EfOrder> Orders { get; set; }
		public DbSet<EfOrderLine> OrderLines { get; set; }
		public DbSet<EfAddress> Addresses { get; set; }
		public DbSet<EfOrderLog> OrderLogs { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EfOrder>()
				.HasMany(order => order.OrderLines)
				.WithRequired(orderline => orderline.Order)
				.HasForeignKey(orderline => orderline.OrderId);

			modelBuilder.Entity<EfOrder>()
				.HasMany(order => order.OrderLogs)
				.WithRequired(log => log.Order)
				.HasForeignKey(log => log.OrderId)
				;

			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			base.OnModelCreating(modelBuilder);
		}
	}
}
