using System;
using System.Threading.Tasks;
using FinancesCore.Business.Models;

namespace FinancesCore.Business.Intefaces
{
    public interface ICategoryRepository: IRepository<Category>
    {
        Task<Category> GetCategory(Guid id);
    }
}
