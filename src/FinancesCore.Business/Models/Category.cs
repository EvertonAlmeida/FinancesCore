using System.Collections.Generic;

namespace FinancesCore.Business.Models
{
    public class Category : Entity
    {
        public string Title { get; set; }

        /* EF Relations */
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}