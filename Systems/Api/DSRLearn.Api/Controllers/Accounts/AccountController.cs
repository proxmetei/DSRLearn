using Asp.Versioning;
using AutoMapper;
using DSRLearn.Common.Security;
using DSRLearn.Services.UserAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSRLearn.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Product")]
    [Route("v{version:apiVersion}/[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserAccountService userAccountService;
        private readonly IMapper mapper;

        public AccountController(IUserAccountService userAccountService, IMapper mapper)
        {
            this.userAccountService = userAccountService;
            this.mapper = mapper;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RequestRegisterAccountModel request)
        {
            (UserAccountModel user, string code) = await userAccountService.Create(mapper.Map<RegisterUserAccountModel>(request));
            
            var callbackUrl = Url.Action(
                nameof(ConfirmEmail),
                "Account",
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);
            
            var result = await userAccountService.SendConfirmation(user.Email, callbackUrl);
            
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            await userAccountService.ConfirmEmail(userId, code);
            
            return Ok();
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await userAccountService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(mapper.Map<ResponseAccountModel>(result));
        }
        [HttpGet("[action]/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await userAccountService.GetByEmail(email);

            if (result == null)
                return NotFound();

            return Ok(mapper.Map<ResponseAccountModel>(result));
        }
        [HttpGet("")]
        public async Task<IEnumerable<ResponseAccountModel>> GetAll()
        {
            var result = await userAccountService.GetAll();
            
            return mapper.Map<IEnumerable<ResponseAccountModel>>(result);
        }
    }
}
