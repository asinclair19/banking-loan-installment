namespace ApiRestLoanInstallment.Models
{
    public class MonthlyFee
    {
        public int MonthlyFeeId { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }

        public int Duration { get; set; }
        public decimal MontlyFee { get; set; }
    }
}
