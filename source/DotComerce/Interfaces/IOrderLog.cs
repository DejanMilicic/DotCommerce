
namespace DotCommerce.Interfaces
{
	using System;
	using DotCommerce.Domain;

	public interface IOrderLog
	{
		Guid OrderId { get; }
		int OrderLineId { get; set; }
		DateTime DateTime { get; }
		LogAction Action { get; }
		string Value { get; }
		string OldValue { get; }
	}
}
