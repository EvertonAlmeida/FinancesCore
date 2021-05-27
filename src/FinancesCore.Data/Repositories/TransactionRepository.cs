using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancesCore.Business.Intefaces;
using FinancesCore.Business.Models;
using FinancesCore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancesCore.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(FinanceCoreDbContext context) : base(context) { }

        public async Task<Transaction> GetTransactionAndCategory(Guid id)
        {
            return await Db.Transactions.AsNoTracking()
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Transaction> GetTransactionByCategory(Guid categoryId)
        {
            return await Db.Transactions.AsNoTracking()
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAndCategories()
        {
            return await Db.Transactions.AsNoTracking().Include(t => t.Category)
                .OrderBy(p => p.CreatedAt).ToListAsync();
        }
    }
}
