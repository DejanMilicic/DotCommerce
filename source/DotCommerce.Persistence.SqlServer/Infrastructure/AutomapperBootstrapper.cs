
namespace DotCommerce.Persistence.SqlServer.Infrastructure
{
	using System.Collections.Generic;

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
				.ForMember(e => e.Status, opt => opt.MapFrom(src => OrderStatus.FromValue(src.Status)))
				//.ConstructProjectionUsing(src => new Order(src.Id, src.UserId, Mapper.Map<List<OrderLine>>(src.OrderLines), OrderStatus.FromValue(src.Status), src.CreatedOn))
				;
			Mapper.CreateMap<Entities.OrderLine, Domain.OrderLine>();

			Mapper.AssertConfigurationIsValid();
		}
	}
}
