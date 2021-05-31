using System.ComponentModel;

namespace FinancesCore.App.ViewModels
{
    public class BalanceViewModel
    {
        [DisplayName("Incoming")]
        public decimal Income { get; set; }

        [DisplayName("Outcoming")]
        public decimal Outcome { get; set; }

        [DisplayName("Total")]
        public decimal Total { get; set; }
    }
}
