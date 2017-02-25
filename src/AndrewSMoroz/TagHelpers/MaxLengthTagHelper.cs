using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsMVC.TagHelpers
{

    // From GitHub at url below
    //https://github.com/VariableNotFound/MaxLengthTagHelper

    [HtmlTargetElement("input", Attributes = "asp-for")]
    public class MaxLengthTagHelper : TagHelper
    {
        public override int Order { get; } = 999;

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);
            if (context.AllAttributes["maxlength"] == null) // Process only if 'maxlength' attr is not present
            {
                var maxLength = GetMaxLength(For.ModelExplorer.Metadata.ValidatorMetadata);
                if (maxLength > 0)
                    output.Attributes.Add("maxlength", maxLength);
            }
        }

        private static int GetMaxLength(IReadOnlyList<object> validatorMetadata)
        {
            for (var i = 0; i < validatorMetadata.Count; i++)
            {
                var stringLengthAttribute = validatorMetadata[i] as StringLengthAttribute;
                if (stringLengthAttribute != null && stringLengthAttribute.MaximumLength > 0)
                {
                    return stringLengthAttribute.MaximumLength;
                }
                var maxLengthAttribute = validatorMetadata[i] as MaxLengthAttribute;
                if (maxLengthAttribute != null && maxLengthAttribute.Length > 0)
                {
                    return maxLengthAttribute.Length;
                }
            }
            return 0;
        }
    }

}
