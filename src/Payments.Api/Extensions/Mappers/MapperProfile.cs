using AutoMapper;
using Payments.Api.Dtos.Requests;
using Payments.Api.Dtos.Response;
using Payments.Domain.Entities;

namespace Payments.Api.Extensions.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Payment, PaymentResponse>();

            CreateMap<PaymentRequest, Payment>();
        }
    }
}
