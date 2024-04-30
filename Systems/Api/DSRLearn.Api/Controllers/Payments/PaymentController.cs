using Asp.Versioning;
using AutoMapper;
using DSRLearn.Services.Logger;
using DSRLearn.Services.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSRLearn.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Product")]
    [Route("v{version:apiVersion}/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IAppLogger logger;
        private readonly IPaymentService paymentService;
        private readonly IMapper mapper;

        public PaymentController(IAppLogger logger, IPaymentService paymentService, IMapper mapper)
        {
            this.logger = logger;
            this.paymentService = paymentService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IEnumerable<ResponsePaymentModel>> GetAll()
        {
            var result = await paymentService.GetAll();

            return mapper.Map<IEnumerable<ResponsePaymentModel>>(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await paymentService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(mapper.Map<ResponsePaymentModel>(result));
        }
        [HttpGet("[action]/{id:Guid}")]
        public async Task<IActionResult> GetByDebtId([FromRoute] Guid id)
        {
            var result = await paymentService.GetByDebtId(id);

            if (result == null)
                return NotFound();

            return Ok(mapper.Map<IEnumerable<ResponsePaymentModel>>(result));
        }

        [HttpPost("")]
        public async Task<ResponsePaymentModel> Create(RequestCreatePaymentModel request)
        {
            var result = await paymentService.Create(mapper.Map<CreatePaymentModel>(request));

            return mapper.Map<ResponsePaymentModel>(result);
        }

        [HttpDelete("{id:Guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await paymentService.Delete(id);
        }

    }
}
