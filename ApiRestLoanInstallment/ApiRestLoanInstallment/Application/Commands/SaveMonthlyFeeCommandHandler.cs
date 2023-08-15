using ApiRestLoanInstallment.Models;
using MediatR;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using ApiRestLoanInstallment.Domain.Commands;
using ApiRestLoanInstallment.Infrastructure.Persistence;

namespace ApiRestLoanInstallment.Application.Commands
{
    public class SaveMonthlyFeeCommandHandler : IRequestHandler<SaveMonthlyFeeCommand, List<MonthlyFee>>
    {
        private readonly FeeDbContext _context;
        //private readonly IMessageProducer _messageProducer;

        public SaveMonthlyFeeCommandHandler(FeeDbContext context)
        {
            _context = context;
        }

        public async Task<List<MonthlyFee>> Handle(SaveMonthlyFeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //MonthlyFee = (Amount * Rate) / (1 - Math.Pow(1 + Rate, - Duration));
                double doubleRate = (double)request.Rate;
                double doubleAmount = (double)request.Amount;
                decimal total = (decimal)(doubleAmount * doubleRate / (1 - Math.Pow(1 + doubleRate, -request.Duration)));

                var fee = new MonthlyFee
                {
                    Amount = request.Amount,
                    Rate = request.Rate,
                    Duration = request.Duration,
                    MontlyFee = total
                };
                _context.MonthlyFees.Add(fee);
                await _context.SaveChangesAsync(cancellationToken);

                //var message = JsonConvert.SerializeObject(fee);
                //_messageProducer.Produce(message);

                return _context.MonthlyFees.Where(a => a.MonthlyFeeId == fee.MonthlyFeeId).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }


    }
}
