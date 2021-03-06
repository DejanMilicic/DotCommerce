﻿
namespace DotCommerce.Infrastructure
{
	using AutoMapper;
	using DotCommerce.Domain;
	using DotCommerce.Infrastructure.EntityFramework.Entities;

	static class AutomapperBootstrapper
	{
		public static void Configure()
		{
			Mapper.CreateMap<EfOrder, Order>()
				.ForMember(e => e.Status, opt => opt.MapFrom(src => OrderStatus.FromValue(src.Status)))
				.ForMember(e => e.EditableOrderLines, opt => opt.MapFrom(src => src.OrderLines))
				.ForMember(e => e.EditableOrderLogs, opt => opt.MapFrom(src => src.OrderLogs))
				.ForMember(e => e.EditableShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
				.ForMember(e => e.EditableBillingAddress, opt => opt.MapFrom(src => src.BillingAddress));

			Mapper.CreateMap<EfAddress, OrderAddress>();

			Mapper.CreateMap<EfOrderLine, OrderLine>();

			Mapper.CreateMap<EfOrderLog, OrderLog>()
				.ForMember(e => e.Action, opt => opt.MapFrom(src => LogAction.FromValue(src.Action)));

			Mapper.AssertConfigurationIsValid();
		}
	}
}
