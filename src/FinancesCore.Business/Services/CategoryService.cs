using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancesCore.Business.Intefaces;
using FinancesCore.Business.Models;
using FinancesCore.Business.Validations;

namespace FinancesCore.Business.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CategoryService(
            ICategoryRepository categoryRepository,
            ITransactionRepository transactionRepository,
            INotifier notifier) : base(notifier)
        {
            _categoryRepository = categoryRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task Add(Category category)
        {
            if (!ExecuteValidation(new CategoryValidation(), category)) return;

            var CategoryAlreadyRegistered = await _categoryRepository.Search(c => c.Title == category.Title);

            if (NotifyIfThereIsAnyCategory(CategoryAlreadyRegistered)) return;

            await _categoryRepository.Add(category);
        }

        public async Task Update(Category category)
        {
            if (!ExecuteValidation(new CategoryValidation(), category)) return;

            var CategoryAlreadyRegistered = await _categoryRepository.Search(c => c.Title == category.Title && c.Id != category.Id);

            if (NotifyIfThereIsAnyCategory(CategoryAlreadyRegistered)) return;

             await _categoryRepository.Update(category);
        }

        public async Task Remove(Guid id)
        {
            var transaction = await _transactionRepository.GetTransactionByCategory(id);

            if (transaction?.Category != null) {
                Notify("The Category is being used by, at least, one Transaction!");
                return;
            }

            await _categoryRepository.Remove(id);
        }

        private bool NotifyIfThereIsAnyCategory(IEnumerable<Category> categories)
        {
            if (categories.Any()) {
                Notify("There's a category with this title already");
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            _categoryRepository?.Dispose();
        }
    }
}
