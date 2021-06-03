using System;
using System.Threading.Tasks;
using FinancesCore.Business.Models;

namespace FinancesCore.Business.Intefaces
{
    public interface ITransactionService : IDisposable
    {
        Task Add(Transaction transaction);
        Task Update(Transaction transaction);
        Task Remove(Guid id);
    }
}