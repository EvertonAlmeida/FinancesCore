using System;
using System.Threading.Tasks;
using FinancesCore.Business.Intefaces;
using FinancesCore.Business.Models;
using FinancesCore.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancesCore.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(FinanceCoreDbContext context) : base(context) { }

        public async Task<Category> GetCategory(Guid id)
        {
            return await Db.Categories.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
