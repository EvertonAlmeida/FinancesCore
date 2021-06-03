using System;
using System.Threading.Tasks;
using FinancesCore.Business.Models;

namespace FinancesCore.Business.Intefaces
{
    public interface ICategoryService : IDisposable
    {
        Task Add(Category category);
        Task Update(Category category);
        Task Remove(Guid id);
    }
}