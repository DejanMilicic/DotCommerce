
namespace DotCommerce.Persistence.SqlServer.Infrastructure
{
	using AutoMapper;

	using DotCommerce.Domain;

	public static class AutomapperBootstrapper
	{
		public static void Configure()
		{
			Mapper.CreateMap<Domain.Order, Entities.Order>();
			Mapper.CreateMap<Domain.OrderLine, Entities.OrderLine>()
				.ForMember(x => x.Order, opt => opt.Ignore());

			Mapper.CreateMap<Entities.Order, Domain.Order>()
				.ForMember(e => e.Status, opt => opt.MapFrom(src => OrderStatus.FromValue(src.Status)));

			Mapper.CreateMap<Entities.OrderLine, Domain.OrderLine>()
				.ForMember(x => x.Weight, opt => opt.Ignore())
				.ForMember(x => x.Price, opt => opt.Ignore());

			Mapper.AssertConfigurationIsValid();
		}
	}
}
