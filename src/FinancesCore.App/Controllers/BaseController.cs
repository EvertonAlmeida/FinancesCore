using FinancesCore.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancesCore.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotifier _notifier;

        protected BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool IsOperationvalid()
        {
            return !_notifier.IsThereAnyNotification();
        }
    }
}
