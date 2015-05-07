DotCommerce.SqlServer
=====================

Steps for integration of DotCommerce with SqlServer persistence layer into your project

1. Run script from DotCommerce folder to create tables needed for DotCommerce
2. Add connection string named "DotCommerce" to the web.config of the project
3. In Dependency injection configuration, register 
DotCommerce.Persistence.SqlServer.Persistence as implementing DotCommerce.Domain.IPersistence
and DotCommerce.API.OrderService as implementing DotCommerce.API.IOrderService
4. Since EF 6.1 created default configuration targeting local database, change this to
	<entityFramework>
		<defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework" />
	</entityFramework>

