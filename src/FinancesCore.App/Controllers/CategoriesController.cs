using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinancesCore.App.ViewModels;
using FinancesCore.Business.Intefaces;
using AutoMapper;
using System.Collections.Generic;
using FinancesCore.Business.Models;
using Microsoft.AspNetCore.Authorization;
using FinancesCore.App.Extensions;

namespace FinancesCore.App.Controllers
{
    [Authorize]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(
            ICategoryRepository categoryRepository,
            ICategoryService categoryService,
            INotifier notifier,
            IMapper mapper) : base(notifier)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAll()));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var categoryViewModel = await GetCategory(id);
            if (categoryViewModel == null) return NotFound();

            return View(categoryViewModel);
        }

        [ClaimsAuthorize("Category", "Add")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Category", "Add")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return View(categoryViewModel);

            var category = _mapper.Map<Category>(categoryViewModel);
            await _categoryService.Add(category);

            if (!IsOperationvalid()) return View(categoryViewModel);
            
            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("Category", "Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var categoryViewModel = await GetCategory(id);
            if (categoryViewModel == null) return NotFound();

            return View(categoryViewModel);
        }

        [ClaimsAuthorize("Category", "Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id) return NotFound();
   
            if (!ModelState.IsValid) return View(categoryViewModel);

            var category = _mapper.Map<Category>(categoryViewModel);
            await _categoryService.Update(category);

            if (!IsOperationvalid()) return View(categoryViewModel);

            return RedirectToAction(nameof(Index));
        }

        [ClaimsAuthorize("Category", "Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var categoryViewModel = await GetCategory(id);
            if (categoryViewModel == null) return NotFound();

            return View(categoryViewModel);
        }

        [ClaimsAuthorize("Category", "Delete")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await GetCategory(id);
            if (category == null) return NotFound();

            await _categoryService.Remove(id);

           if (!IsOperationvalid()) return View(category);

            TempData["Success"] = "Category successfully deleted";

            return RedirectToAction(nameof(Index));
        }

        private async Task<CategoryViewModel> GetCategory(Guid id)
        {
            return _mapper.Map<CategoryViewModel>(await _categoryRepository.GetCategory(id));
        }
    }
}
