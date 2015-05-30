
namespace DotCommerce.Interfaces
{
	using System;
	using DotCommerce.Domain;

	public interface IOrderLog
	{
		int OrderId { get; }
		DateTime DateTime { get; }
		LogAction Action { get; }
		string Value { get; }
		string OldValue { get; }
	}
}
