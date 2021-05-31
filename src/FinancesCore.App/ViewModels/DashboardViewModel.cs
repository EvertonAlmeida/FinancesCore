using System.Collections.Generic;

namespace FinancesCore.App.ViewModels
{
    public class DashboardViewModel
    {
        public BalanceViewModel Balance { get; set; }
        public TransactionViewModel Transaction { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}
