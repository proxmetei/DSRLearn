using AutoMapper;
using DSRLearn.Services.Payments;

namespace DSRLearn.Api.Controllers
{
    public class RequestCreatePaymentModel
    {
        public Guid DebtId { get; set; }
        public int Amount { get; set; }
        public DateOnly Date { get; set; }
    }
    public class RequestCreatePaymentModelProfile : Profile
    {
        public RequestCreatePaymentModelProfile()
        {
            CreateMap<RequestCreatePaymentModel, CreatePaymentModel>();
        }
    }
}
