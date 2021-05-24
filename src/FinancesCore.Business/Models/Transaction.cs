using System;

namespace FinancesCore.Business.Models
{
    public class Transaction: Entity
    {
        public string Title { get; set; }
        public decimal Value { get; set; }
        public TypeTransactionEnum Type { get; set; }
        public bool Ativo { get; set; }
        public Guid CategoryId { get; set; }

        /* EF Relations */
        public Transaction Category { get; set; }
    }
}