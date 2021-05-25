using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinancesCore.App.ViewModels
{
    public class TransactionViewModel
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

        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Value { get; set; }

        public int Type { get; set; }

        [DisplayName("Active?")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [DisplayName("Category")]
        public Guid CategoryId { get; set; }

        public CategoryViewModel Category { get; set; }

    }
}
