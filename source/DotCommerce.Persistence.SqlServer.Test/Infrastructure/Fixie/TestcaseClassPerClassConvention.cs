
namespace DotCommerce.Persistence.SqlServer.Test.Infrastructure.Fixie
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	using global::DotCommerce.Persistence.SqlServer.Test.Infrastructure.Autofixture;

	using global::Fixie;
	using Ploeh.AutoFixture.Kernel;
	using Fixture = Ploeh.AutoFixture.Fixture;

	public class TestcaseClassPerClassConvention : Convention
	{
		public TestcaseClassPerClassConvention()
		{
			this.Classes
				.NameEndsWith("Tests")
				.Where(t =>
					t.GetConstructors()
					.All(ci => ci.GetParameters().Length == 0)
				);

			this.Methods.Where(mi => mi.IsPublic && mi.IsVoid());

			this.Parameters.Add(this.FillFromFixture);
		}

		private IEnumerable<object[]> FillFromFixture(MethodInfo method)
		{
			Fixture fixture = (Fixture)new Fixture().Customize(new Customizations());

			yield return this.GetParameterData(method.GetParameters(), fixture);
		}

		private object[] GetParameterData(ParameterInfo[] parameters, Fixture fixture)
		{
			return parameters
				.Select(p => new SpecimenContext(fixture).Resolve(p.ParameterType))
				.ToArray();
		}
	}
}