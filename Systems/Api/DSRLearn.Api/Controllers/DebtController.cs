using Asp.Versioning;
using DSRLearn.Services.Debts;
using DSRLearn.Services.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSRLearn.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Product")]
    [Route("v{version:apiVersion}/[controller]")]
    public class DebtController : ControllerBase
    {
        private readonly IAppLogger logger;
        private readonly IDebtService debtService;

        public DebtController(IAppLogger logger, IDebtService debtService)
        {
            this.logger = logger;
            this.debtService = debtService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<DebtModel>> GetAll()
        {
            var result = await debtService.GetAll();

            return result;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await debtService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("[action]/{id:Guid}")]
        public async Task<IActionResult> GetByCreditorId([FromRoute] Guid id)
        {
            var result = await debtService.GetByCreditorId(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpGet("[action]/{id:Guid}")]
        public async Task<IActionResult> GetByDebtorId([FromRoute] Guid id)
        {
            var result = await debtService.GetByDebtorId(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<DebtModel> Create(CreateDebtModel request)
        {
            var result = await debtService.Create(request);

            return result;
        }

        [HttpPut("{id:Guid}")]
        public async Task Update([FromRoute] Guid id, UpdateDebtModel request)
        {
            await debtService.Update(id, request);
        }

        [HttpDelete("{id:Guid}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await debtService.Delete(id);
        }

    }
}
