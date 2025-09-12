using AutoMapper;
using Payments.Api.Dtos.Requests;
using Payments.Domain.Entities;

namespace Payments.Api.Extensions.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            //CreateMap<PaymentRequest, Payment>()
            //     .ForMember(destination => destination.UserName, options => options.MapFrom(source => source.Email))
            //     .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
            //     .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.FirstName))
            //     .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.LastName))
            //     .ForMember(destination => destination.Birthdate, options => options.MapFrom(source => source.Birthdate))
            //     .ForMember(destination => destination.NickName, options => options.MapFrom(source => source.NickName));
               
        }
    }
}
