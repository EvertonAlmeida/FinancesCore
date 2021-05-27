using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinancesCore.App.ViewModels;
using FinancesCore.Business.Intefaces;
using AutoMapper;
using System.Collections.Generic;
using FinancesCore.Business.Models;

namespace FinancesCore.App.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(
            ICategoryRepository categoryRepository, 
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAll()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var categoryViewModel = await GetCategory(id);
            if (categoryViewModel == null) return NotFound();

            return View(categoryViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid) return View(categoryViewModel);

            var category = _mapper.Map<Category>(categoryViewModel);
            await _categoryRepository.Add(category);
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var categoryViewModel = await GetCategory(id);
            if (categoryViewModel == null) return NotFound();

            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id) return NotFound();
   
            if (!ModelState.IsValid) return View(categoryViewModel);

            var category = _mapper.Map<Category>(categoryViewModel);
            await _categoryRepository.Update(category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var categoryViewModel = await GetCategory(id);
            if (categoryViewModel == null) return NotFound();

            return View(categoryViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await GetCategory(id);
            if (category == null) return NotFound();

            await _categoryRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<CategoryViewModel> GetCategory(Guid id)
        {
            return _mapper.Map<CategoryViewModel>(await _categoryRepository.GetById(id));
        }
    }
}
