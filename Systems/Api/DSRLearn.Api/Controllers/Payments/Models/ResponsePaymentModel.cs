using AutoMapper;
using DSRLearn.Services.Payments;

namespace DSRLearn.Api.Controllers
{
    public class ResponsePaymentModel
    {
        public Guid Id { get; set; }
        public Guid DebtId { get; set; }
        public int Amount { get; set; }
        public DateOnly Date { get; set; }
    }
    public class ResponsePaymentModelProfile : Profile
    {
        public ResponsePaymentModelProfile()
        {
            CreateMap<PaymentModel, ResponsePaymentModel>();
        }
    }
}
