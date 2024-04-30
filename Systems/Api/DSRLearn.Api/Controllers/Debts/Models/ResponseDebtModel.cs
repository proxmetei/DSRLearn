using AutoMapper;
using DSRLearn.Services.Debts;

namespace DSRLearn.Api.Controllers
{
    public class ResponseDebtModel
    {
        public Guid Id { get; set; }
        public Guid DebtorId { get; set; }
        public Guid CreditorId { get; set; }
        public int Amount { get; set; }
        public DateOnly RepaidDate { get; set; }
    }
    public class ResponseDebtModelProfile : Profile
    {
        public ResponseDebtModelProfile()
        {
            CreateMap<DebtModel, ResponseDebtModel>();
        }

    }
}
