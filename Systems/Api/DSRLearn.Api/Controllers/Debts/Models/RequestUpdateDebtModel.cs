using AutoMapper;
using DSRLearn.Services.Debts;

namespace DSRLearn.Api.Controllers
{
    public class RequestUpdateDebtModel
    {
        public int Amount { get; set; }
        public Guid Id { get; set; }
        public DateOnly RepaidDate { get; set; }
    }

    public class RequestUpdateDebtModelProfile : Profile
    {
        public RequestUpdateDebtModelProfile()
        {
            CreateMap<RequestUpdateDebtModel, UpdateDebtModel>();
        }
    }
}
