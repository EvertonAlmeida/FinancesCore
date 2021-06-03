using System;
using System.Linq;
using System.Threading.Tasks;
using FinancesCore.Business.Intefaces;
using FinancesCore.Business.Models;
using FinancesCore.Business.Validations;

namespace FinancesCore.Business.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        private readonly ITransactionRepository _transactionsRepository;

        public TransactionService(
            ITransactionRepository transactionsRepository,
            INotifier notifier) : base(notifier)
        {
            _transactionsRepository = transactionsRepository;
        }

        public async Task Add(Transaction transaction)
        {
            if (!ExecuteValidation(new TransactionValidation(), transaction)) return;

            if (transaction.Type == TypeTransactionEnum.Outcome) {
                
                if (!await OutcomeValidation(transaction.Value)) {
                    Notify("Insufficient Fund");
                    return;
                }
            }
     

            await _transactionsRepository.Add(transaction);
        }

        public async Task Update(Transaction transaction)
        {
            if (!ExecuteValidation(new TransactionValidation(), transaction)) return;

            await _transactionsRepository.Add(transaction);
        }

        public async Task Remove(Guid id)
        {
            await _transactionsRepository.Remove(id);
        }  

        public void Dispose()
        {
            _transactionsRepository?.Dispose();
        }

        private async Task<bool> OutcomeValidation(decimal value)
        {
            var transactions = await _transactionsRepository.GetAll();

            var income = transactions.Sum(t => t.Type == TypeTransactionEnum.Income ? t.Value : 0);
            var outcome = transactions.Sum(t => t.Type == TypeTransactionEnum.Outcome ? t.Value : 0);
            var total = income - outcome;

            return total - value >= 0;
        }
    }
}
