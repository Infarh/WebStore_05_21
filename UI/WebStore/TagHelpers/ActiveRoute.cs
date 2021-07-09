using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = AttributeName)]
    public class ActiveRoute : TagHelper
    {
        private const string AttributeName = "ws-is-active-route";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll(AttributeName);
        }
    }
}
