using AutoMapper;
using DSRLearn.Services.UserAccount;

namespace DSRLearn.Api.Controllers
{
    public class ResponseAccountModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }

    public class ResponseAccountModelProfile : Profile
    {
        public ResponseAccountModelProfile()
        {
            CreateMap<UserAccountModel, ResponseAccountModel>();
        }
    }
}
