
namespace DotCommerce.Infrastructure.EntityFramework.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("DotCommerce_OrderLog")]
	class EfOrderLog
	{
		public int Id { get; set; }
		public DateTime DateTime { get; set; }
		public string Action { get; set; }
		public string Value { get; set; }
		public string OldValue { get; set; }

		// Navigation properties
		public int OrderId { get; set; }
		public EfOrder Order { get; set; }
	}
}
