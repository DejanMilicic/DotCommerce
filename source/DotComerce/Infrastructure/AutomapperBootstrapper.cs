
namespace DotCommerce.Infrastructure
{
	using AutoMapper;

	using DotCommerce.Domain;
	using DotCommerce.Infrastructure.EntityFramework.Entities;
	using DotCommerce.Interfaces;

	static class AutomapperBootstrapper
	{
		public static void Configure()
		{
			Mapper.CreateMap<Order, EfOrder>()
				.ForMember(x => x.OrderLines, opt => opt.MapFrom(src => src.EditableOrderLines));

			Mapper.CreateMap<OrderLine, EfOrderLine>()
				.ForMember(x => x.Order, opt => opt.Ignore())
				.ForMember(x => x.OrderId, opt => opt.Ignore());

			Mapper.CreateMap<EfOrder, Order>()
				.ForMember(e => e.Status, opt => opt.MapFrom(src => OrderStatus.FromValue(src.Status)))
				.ForMember(e => e.EditableOrderLines, opt => opt.MapFrom(src => src.OrderLines));

			Mapper.CreateMap<EfOrderLine, OrderLine>()
				.ForMember(x => x.Weight, opt => opt.Ignore())
				.ForMember(x => x.Price, opt => opt.Ignore());

			Mapper.AssertConfigurationIsValid();
		}
	}
}
