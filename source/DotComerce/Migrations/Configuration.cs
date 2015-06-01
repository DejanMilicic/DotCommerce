
namespace DotCommerce.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DotCommerce.Infrastructure.EntityFramework.Db>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
