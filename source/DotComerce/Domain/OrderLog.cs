﻿
namespace DotCommerce.Domain
{
	using System;
	using DotCommerce.Interfaces;

	class OrderLog : IOrderLog
	{
		public int Id { get; set; }
		public Guid OrderId { get; set; }
		public int OrderLineId { get; set; }
		public DateTime DateTime { get; set; }
		public LogAction Action { get; set; }
		public string Value { get; set; }
		public string OldValue { get; set; }
	}
}
