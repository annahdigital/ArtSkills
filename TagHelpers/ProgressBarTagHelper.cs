using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ArtSkills.TagHelpers
{
    [HtmlTargetElement("div", Attributes = ProgressValueAttributeName)]
    public class ProgressBarTagHelper : TagHelper
    {
        private const string ProgressValueAttributeName = "progress-value";
        private const string ProgressMaxAttributeName = "progress-max";

        [HtmlAttributeName(ProgressValueAttributeName)]
        public int ProgressValue { get; set; }
        public const int progressMin = 0;
        [HtmlAttributeName(ProgressMaxAttributeName)]
        public int progressMax {get; set;}

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var progressPercentage = Math.Round(((decimal)(ProgressValue) / (decimal)progressMax) * 100, 4);

            string progressBarContent =
            $@"<div class='progress-bar bg-success' role='progressbar' aria-valuenow='{ProgressValue}' 
                aria-valuemin='{progressMin}' aria-valuemax='{progressMax}' style='width: {progressPercentage}%; padding:5px;'> 
               <span class='text-center'>{progressPercentage}% </span>
            </div>";

            output.Content.AppendHtml(progressBarContent);
            string classValue;
            if (output.Attributes.ContainsName("class"))        //adding class to the outer div
            {
                classValue = string.Format("{0} {1}", output.Attributes["class"].Value, "progress");
            }
            else
            {
                classValue = "progress";
            }

            output.Attributes.SetAttribute("class", classValue);
        }
    }
}
