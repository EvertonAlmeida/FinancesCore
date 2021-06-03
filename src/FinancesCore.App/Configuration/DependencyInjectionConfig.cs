using FinancesCore.Business.Intefaces;
using FinancesCore.Business.Notifications;
using FinancesCore.Business.Services;
using FinancesCore.Data.Context;
using FinancesCore.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinancesCore.App.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<FinanceCoreDbContext>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}
