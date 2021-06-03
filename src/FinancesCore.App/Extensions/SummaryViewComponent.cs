using System.Threading.Tasks;
using FinancesCore.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancesCore.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotifier _notifier;

        public SummaryViewComponent(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notification = await Task.FromResult(_notifier.GetNotifications());
            notification.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Message));

            return View();
        }
    }
}
