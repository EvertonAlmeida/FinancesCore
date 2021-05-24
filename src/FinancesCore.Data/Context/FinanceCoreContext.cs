using FinancesCore.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancesCore.Data.Context
{
    public class FinanceCoreContext : DbContext
    {
        public FinanceCoreContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transaction> Categories { get; set; }
    }
}
