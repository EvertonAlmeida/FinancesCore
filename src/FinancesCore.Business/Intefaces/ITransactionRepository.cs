using System;
using System.Threading.Tasks;
using FinancesCore.Business.Models;

namespace FinancesCore.Business.Intefaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<Transaction> GetTransactionAndCategory(Guid id);
        Task<Transaction> GetTransactionByCategory(Guid categoryId);
    }
}
