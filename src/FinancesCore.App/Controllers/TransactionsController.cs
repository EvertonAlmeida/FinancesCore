using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinancesCore.App.ViewModels;
using AutoMapper;
using FinancesCore.Business.Intefaces;
using FinancesCore.Business.Models;
using System.Linq;

namespace FinancesCore.App.Controllers
{
    public class TransactionsController : BaseController
    {
        private readonly ITransactionRepository _transactionsRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionsController(
            ITransactionRepository transactionsRepository,
            ICategoryRepository categoryRepository,
            ITransactionService transactionService,
            INotifier notifier,
            IMapper mapper) : base(notifier)
        {
            _transactionsRepository = transactionsRepository;
            _categoryRepository = categoryRepository;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(GetDashboard(await GetTransactionsAndCategories()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var transactionViewModel = await GetTransaction(id);
            if (transactionViewModel == null) return NotFound();

            return View(transactionViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var transactionViewModel = await LoadCategoriesIntoTransaction(new TransactionViewModel());
            return View(transactionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionViewModel transactionViewModel)
        {
            transactionViewModel = await LoadCategoriesIntoTransaction(transactionViewModel);
            if (!ModelState.IsValid) return View(transactionViewModel);

            var transaction = _mapper.Map<Transaction>(transactionViewModel);
            await _transactionService.Add(transaction);

            if (!IsOperationvalid()) return View(transactionViewModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var transactionViewModel = await GetTransaction(id);
            if (transactionViewModel == null) return NotFound();

            return View(transactionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TransactionViewModel transactionViewModel)
        {
            if (id != transactionViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(transactionViewModel);

            var transaction = _mapper.Map<Transaction>(transactionViewModel);
            await _transactionService.Update(transaction);

            if (!IsOperationvalid()) return View(transactionViewModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var transactionViewModel = await GetTransactionAndCategory(id);
            if (transactionViewModel == null) return NotFound();

            return View(transactionViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var transaction = await GetTransaction(id);
            if (transaction == null) return NotFound();

            await _transactionService.Remove(id);

            if (!IsOperationvalid()) return View(transaction);

            return RedirectToAction(nameof(Index));
        }

        private async Task<TransactionViewModel> GetTransaction(Guid id)
        {
            var transaction = _mapper.Map<TransactionViewModel>(await _transactionsRepository.GetTransactionAndCategory(id));
            transaction = await LoadCategoriesIntoTransaction(transaction);
            return transaction;
        }

        private async Task<TransactionViewModel> GetTransactionAndCategory(Guid id)
        {
            return _mapper.Map<TransactionViewModel>(await _transactionsRepository.GetTransactionAndCategory(id));
        }

        private async Task<TransactionViewModel> LoadCategoriesIntoTransaction(TransactionViewModel transaction)
        {
            transaction.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAll());
            return transaction;
        }

        private async Task<IEnumerable<TransactionViewModel>> GetTransactionsAndCategories()
        {
            return _mapper.Map<IEnumerable<TransactionViewModel>>(await _transactionsRepository.GetTransactionsAndCategories());
        }

        private DashboardViewModel GetDashboard(IEnumerable<TransactionViewModel> transactions)
        {
           var dashboard = new DashboardViewModel() {
              Transactions = transactions,
               Balance = GetBalance(transactions)
           };

            return dashboard;
        }

        private BalanceViewModel GetBalance(IEnumerable<TransactionViewModel> transactions)
        {
            var income = transactions.Sum(t => t.Type == 1 ? t.Value : 0);
            var outcome = transactions.Sum(t => t.Type == 2 ? t.Value : 0);
            var total = income - outcome;

            var balance = new BalanceViewModel() {
                Outcome = outcome,
                Income = income,
                Total = total,
            };

            return balance;
        }
    }
}
