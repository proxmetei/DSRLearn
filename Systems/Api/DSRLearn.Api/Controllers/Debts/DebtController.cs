using System.Security.Claims;
using Asp.Versioning;
using AutoMapper;
using DSRLearn.Api.Controllers.Debts.Models;
using DSRLearn.Common.Security;
using DSRLearn.Services.Debts;
using DSRLearn.Services.Logger;
using DSRLearn.Services.Payments;
using DSRLearn.Services.UserAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSRLearn.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Product")]
    [Route("v{version:apiVersion}/[controller]")]
    public class DebtController : ControllerBase
    {
        private readonly IAppLogger logger;
        private readonly IDebtService debtService;
        private readonly IMapper mapper;
        private readonly IUserAccountService userAccountService;
        private readonly IPaymentService paymentService;

        public DebtController(IAppLogger logger, IDebtService debtService,
            IMapper mapper, IUserAccountService userAccountService,
            IPaymentService paymentService)
        {
            this.logger = logger;
            this.debtService = debtService;
            this.mapper = mapper;
            this.userAccountService = userAccountService;
            this.paymentService = paymentService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<ResponseDebtModel>> GetAll()
        {
            string email = HttpContext.User.FindAll(ClaimTypes.Name).FirstOrDefault().Value;

            var user = await userAccountService.GetByEmail(email);

            var result = await debtService.GetByDebtorId(user.Id);

            result = result.Concat(await debtService.GetByCreditorId(user.Id));

            return mapper.Map<IEnumerable<ResponseDebtModel>>(result);
        }
        [HttpGet("[action]")]
        public async Task<IEnumerable<ResponseDebtModel>> GetDebts()
        {
            string email = HttpContext.User.FindAll(ClaimTypes.Name).FirstOrDefault().Value;

            var user = await userAccountService.GetByEmail(email);

            var result = await debtService.GetByCreditorId(user.Id);

            return mapper.Map<IEnumerable<ResponseDebtModel>>(result);
        }
        [HttpGet("[action]")]
        public async Task<IEnumerable<ResponseDebtModel>> GetCredits()
        {
            string email = HttpContext.User.FindAll(ClaimTypes.Name).FirstOrDefault().Value;

            var user = await userAccountService.GetByEmail(email);

            var result = await debtService.GetByDebtorId(user.Id);

            return mapper.Map<IEnumerable<ResponseDebtModel>>(result);
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await debtService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(mapper.Map<ResponseDebtModel>(result));
        }
        [HttpGet("[action]/{id:Guid}")]
        public async Task<IActionResult> GetByCreditorId([FromRoute] Guid id)
        {
            var result = await debtService.GetByCreditorId(id);

            if (result == null)
                return NotFound();

            return Ok(mapper.Map<ResponseDebtModel>(result));
        }
        [HttpGet("[action]/{id:Guid}")]
        public async Task<IActionResult> GetByDebtorId([FromRoute] Guid id)
        {
            var result = await debtService.GetByDebtorId(id);

            if (result == null)
                return NotFound();

            return Ok(mapper.Map<ResponseDebtModel>(result));
        }

        [HttpPut("[action]")]
        public async Task CreateNetting(RequestNettingDebtModel request)
        {
            var paymentRequest1 = new RequestCreatePaymentModel
            {
                DebtId = request.Id1,
                Amount = request.Amount,
                Date = DateOnly.FromDateTime(DateTime.Now)
            };

            var paymentRequest2 = new RequestCreatePaymentModel
            {
                DebtId = request.Id2,
                Amount = request.Amount,
                Date = DateOnly.FromDateTime(DateTime.Now)
            };

            await paymentService.Create(mapper.Map<CreatePaymentModel>(paymentRequest1));

            await paymentService.Create(mapper.Map<CreatePaymentModel>(paymentRequest2));
        }
        [HttpPost("")]
        public async Task<DebtModel> Create(RequestCreateDebtModel request)
        {
            var result = await debtService.Create(mapper.Map<CreateDebtModel>(request));

            return mapper.Map<DebtModel>(result);
        }

        [HttpPut("")]
        public async Task Update(RequestUpdateDebtModel request)
        {
            await debtService.Update(request.Id, mapper.Map<UpdateDebtModel>(request));
        }

        [HttpDelete("{id:Guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await debtService.Delete(id);
        }

    }
}
