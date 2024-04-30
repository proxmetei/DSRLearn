using AutoMapper;
using DSRLearn.Services.UserAccount;

namespace DSRLearn.Api.Controllers
{
    public class RequestRegisterAccountModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RequestRegisterAccountModelProfile : Profile
    {
        public RequestRegisterAccountModelProfile()
        {
            CreateMap<RequestRegisterAccountModel, RegisterUserAccountModel>();
        }
    }
}
