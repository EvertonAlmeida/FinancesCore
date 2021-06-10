using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FinancesCore.App.Extensions
{
    [HtmlTargetElement("*", Attributes = "display-by-transaction-type")]
    public class TransactionTypeTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TransactionTypeTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("display-by-transaction-type")]
        public int TransactionType { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));


            if (TransactionType == 1) {
                output.Attributes.Add(new TagHelperAttribute("class", "fas fa-arrow-down"));
                output.Attributes.Add(new TagHelperAttribute("style", "color: forestgreen"));
            }

            if (TransactionType == 2) {
                output.Attributes.Add(new TagHelperAttribute("class", "fas fa-arrow-up"));
                output.Attributes.Add(new TagHelperAttribute("style", "color: red"));
            }
        }
    }
}