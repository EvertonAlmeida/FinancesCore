using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinancesCore.App.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? UpdatedAt { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(200, ErrorMessage = "The field {0} need to have between {2} and {1} caracters", MinimumLength = 2)]
        public string Title { get; set; }

        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}
