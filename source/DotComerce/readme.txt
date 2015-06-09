DotCommerce
===========

Steps for integration of DotCommerce with SqlServer persistence layer into your project

1. Add connection string named "DotCommerce" to the web.config of the project
2. In Dependency injection configuration, register DotCommerceApi as implementing IDotCommerceApi
3. Implement IShippingCalculator and register in DependencyInjection
4. Since EF 6.1 created default configuration targeting local database, change this to
	<entityFramework>
		<defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework" />
	</entityFramework>

