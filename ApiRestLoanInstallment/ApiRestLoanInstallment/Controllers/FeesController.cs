using ApiRestLoanInstallment.Features.MonthlyFees.Dto;
using ApiRestLoanInstallment.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestLoanInstallment.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class FeesController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public FeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<List<MonthlyFee>>> ReturnMonthlyFee(SaveMonthlyFeeCommand command)
        {
            var monthlyFee = await _mediator.Send(command);
            return Ok(monthlyFee);
        }

    }
}
