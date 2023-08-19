using ApiRestLoanInstallment.Models;
using MediatR;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;
using ApiRestLoanInstallment.Domain.Commands;
using ApiRestLoanInstallment.Infrastructure.Persistence;
using ApiRestLoanInstallment.Infrastructure;

namespace ApiRestLoanInstallment.Application.Commands
{
    public class SaveMonthlyFeeCommandHandler : IRequestHandler<SaveMonthlyFeeCommand, List<MonthlyFee>>
    {
        private readonly FeeDbContext _context;
        private readonly IMessageProducer _messageProducer;

        public SaveMonthlyFeeCommandHandler(FeeDbContext context, IMessageProducer messageProducer)
        {
            _context = context;
            _messageProducer = messageProducer;
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

                //for return new monthly fee
                var newFee = _context.MonthlyFees.Where(a => a.MonthlyFeeId == fee.MonthlyFeeId).ToList();
                
                //RabbitMQ
                var message = JsonConvert.SerializeObject(newFee);
                _messageProducer.Produce(message);

                return newFee;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }


    }
}
