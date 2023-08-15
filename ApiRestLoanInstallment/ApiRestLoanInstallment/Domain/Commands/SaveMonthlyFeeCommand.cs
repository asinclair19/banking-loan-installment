using ApiRestLoanInstallment.Models;
using MediatR;

namespace ApiRestLoanInstallment.Domain.Commands
{
    public class SaveMonthlyFeeCommand : IRequest<List<MonthlyFee>>
    {
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }

        public int Duration { get; set; }
        //public decimal MontlyFee { get; set; }
    }
}
