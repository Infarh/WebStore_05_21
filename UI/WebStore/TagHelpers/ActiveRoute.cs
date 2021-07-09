using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore.TagHelpers
{
    [HtmlTargetElement(Attributes = AttributeName)]
    public class ActiveRoute : TagHelper
    {
        private const string AttributeName = "ws-is-active-route";

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public Dictionary<string, string> RouteValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsActive())
                MakeActive(output);

            output.Attributes.RemoveAll(AttributeName);
        }

        private bool IsActive()
        {


            return true;
        }

        private static void MakeActive(TagHelperOutput output)
        {

        }
    }
}
