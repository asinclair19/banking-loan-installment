using ApiRestLoanInstallment.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRestLoanInstallment
{
    public class FeeDbContext : DbContext
    {
        public FeeDbContext(DbContextOptions options) : base(options) { }

        public DbSet<MonthlyFee> MonthlyFees { get; set; }
    }
}
