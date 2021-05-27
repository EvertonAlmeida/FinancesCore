using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinancesCore.App.ViewModels;
using AutoMapper;
using FinancesCore.Business.Intefaces;
using FinancesCore.Business.Models;

namespace FinancesCore.App.Controllers
{
    public class TransactionsController : BaseController
    {
        private readonly ITransactionRepository _transactionsRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public TransactionsController(
            ITransactionRepository transactionsRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _transactionsRepository = transactionsRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetTransactionAndCategories());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var transactionViewModel = await GetTransaction(id);
            if (transactionViewModel == null) return NotFound();

            return View(transactionViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var transactionViewModel = await LoadCategories(new TransactionViewModel());
            return View(transactionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionViewModel transactionViewModel)
        {
            transactionViewModel = await LoadCategories(transactionViewModel);
            if (!ModelState.IsValid) return View(transactionViewModel);

            var transaction = _mapper.Map<Transaction>(transactionViewModel);
            await _transactionsRepository.Add(transaction);

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
            await _transactionsRepository.Update(transaction);

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

            await _transactionsRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<TransactionViewModel> GetTransaction(Guid id)
        {
            return _mapper.Map<TransactionViewModel>(await _transactionsRepository.GetById(id));
        }

        private async Task<TransactionViewModel> GetTransactionAndCategory(Guid id)
        {
            return _mapper.Map<TransactionViewModel>(await _transactionsRepository.GetTransactionAndCategory(id));
        }

        private async Task<TransactionViewModel> LoadCategories(TransactionViewModel transaction)
        {
            transaction.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAll());
            return transaction;
        }

        private async Task<IEnumerable<TransactionViewModel>> GetTransactionAndCategories()
        {
            return _mapper.Map<IEnumerable<TransactionViewModel>>(await _transactionsRepository.GetTransactionsAndCategories());
        }
    }
}
