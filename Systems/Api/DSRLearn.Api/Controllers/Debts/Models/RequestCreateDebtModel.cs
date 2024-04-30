using AutoMapper;
using DSRLearn.Services.Debts;

namespace DSRLearn.Api.Controllers
{
    public class RequestCreateDebtModel
    {
        public Guid DebtorId { get; set; }
        public Guid CreditorId { get; set; }
        public int Amount { get; set; }
        public DateOnly RepaidDate { get; set; }
    }
    public class RequestCreateDebtModelProfile : Profile
    {
        public RequestCreateDebtModelProfile()
        {
            CreateMap<RequestCreateDebtModel, CreateDebtModel>();
        }

    }
}
