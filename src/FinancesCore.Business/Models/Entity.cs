using System;

namespace FinancesCore.Business.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}