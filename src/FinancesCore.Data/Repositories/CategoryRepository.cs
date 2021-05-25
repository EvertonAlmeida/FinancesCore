using FinancesCore.Business.Intefaces;
using FinancesCore.Business.Models;
using FinancesCore.Data.Context;

namespace FinancesCore.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(FinanceCoreDbContext context) : base(context) { }
    }
}
