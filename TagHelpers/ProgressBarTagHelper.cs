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
            decimal progressPercentage;
            if (progressMax != 0)
                progressPercentage = Math.Round(((decimal)(ProgressValue) / (decimal)progressMax) * 100, 4);
            else
               progressPercentage = 0;
            int progressRounded = Decimal.ToInt32(progressPercentage);

            string progressBarContent =
            $@"<div class='progress-bar bg-success' role='progressbar' aria-valuenow='{progressRounded}' 
                aria-valuemin='{progressMin}' aria-valuemax='100' style='width: {progressRounded}%; padding:5px;'> 
               <span class='text-center' style='width: {progressRounded}%'>{progressPercentage}% Complete</span>
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
