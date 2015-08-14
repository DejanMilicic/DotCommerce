
namespace DotCommerce.Persistence.SqlServer.Test.Infrastructure.DTO
{
	using System;
	using global::DotCommerce.Domain;

	public class TestOrderLog
	{
		public TestOrderLog()
		{
			this.OldValue = null;
			this.Value = null;
		}

		public Guid OrderId { get; set; }
		public int OrderLineId { get; set; }
		public LogAction Action { get; set; }
		public string OldValue { get; set; }
		public string Value { get; set; }
	}
}