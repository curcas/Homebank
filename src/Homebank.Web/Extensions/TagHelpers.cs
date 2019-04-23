using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace Homebank.Web.Extensions
{
    [HtmlTargetElement("input", Attributes = AttributeName)]
    [HtmlTargetElement("textarea", Attributes = AttributeName)]
    public class InvalidTagHelper : TagHelper
    {
        private const string AttributeName = "asp-invalid-for";

        private readonly IActionContextAccessor _accessor;

        public InvalidTagHelper(IActionContextAccessor accessor)
        {
            _accessor = accessor;
        }

        [HtmlAttributeName(AttributeName)]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_accessor.ActionContext.ModelState.GetFieldValidationState(For.Name) == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                output.AddClass("is-invalid", HtmlEncoder.Default);
            }
        }
    }
}
