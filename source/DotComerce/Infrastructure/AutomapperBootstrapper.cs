
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
				.ForMember(e => e.EditableOrderLines, opt => opt.MapFrom(src => src.OrderLines));

			Mapper.CreateMap<EfOrderLine, OrderLine>();

			Mapper.AssertConfigurationIsValid();
		}
	}
}
