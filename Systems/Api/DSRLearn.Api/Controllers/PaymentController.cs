using Asp.Versioning;
using DSRLearn.Services.Logger;
using DSRLearn.Services.Payments;
using Microsoft.AspNetCore.Mvc;

namespace DSRLearn.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Product")]
    [Route("v{version:apiVersion}/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IAppLogger logger;
        private readonly IPaymentService paymentService;

        public PaymentController(IAppLogger logger, IPaymentService paymentService)
        {
            this.logger = logger;
            this.paymentService = paymentService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<PaymentModel>> GetAll()
        {
            var result = await paymentService.GetAll();

            return result;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await paymentService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("[action]/{id:Guid}")]
        public async Task<IActionResult> GetByDebtId([FromRoute] Guid id)
        {
            var result = await paymentService.GetByDebtId(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<PaymentModel> Create(CreatePaymentModel request)
        {
            var result = await paymentService.Create(request);

            return result;
        }

        [HttpDelete("{id:Guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await paymentService.Delete(id);
        }

    }
}
