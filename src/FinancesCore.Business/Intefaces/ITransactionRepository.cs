using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinancesCore.Business.Models;

namespace FinancesCore.Business.Intefaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<Transaction> GetTransactionAndCategory(Guid id);
        Task<IEnumerable<Transaction>> GetTransactionsAndCategories();
        Task<Transaction> GetTransactionByCategory(Guid categoryId);
    }
}
