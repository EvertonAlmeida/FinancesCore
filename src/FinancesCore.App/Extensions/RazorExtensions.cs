using Microsoft.AspNetCore.Mvc.Razor;

namespace FinancesCore.App.Extensions
{
    public static class RazorExtensions
    {
        public static string GetTypeDescription(this RazorPage page, int typeTransaction)
        {
            return typeTransaction == 1 ? "Incoming" : "Outcoming";
        }
    }
}