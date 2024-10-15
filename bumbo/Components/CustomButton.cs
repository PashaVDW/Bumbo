using Microsoft.AspNetCore.Razor.TagHelpers;

namespace bumbo.Components
{
    [HtmlTargetElement("default-button")]
    public class CustomButton : TagHelper
    {
        public string ButtonText { get; set; } = "klik hier";
        public string ButtonType { get; set; }
        public string OnClick { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";

            string buttonClass = ButtonType?.ToLower() switch
            {
                "primary" => "primary_bg_color hover:bg-yellow-400 text-white font-semibold py-2 px-6 rounded-xl",
                "secondary" => "bg-gray-600 hover:bg-gray-500 text-white font-semibold py-2 px-6 rounded-xl",
                _ => "primary_bg_color hover:bg-yellow-400 text-white font-semibold py-2 px-6 rounded-xl"
            };
            output.Attributes.SetAttribute("class", buttonClass);
            if (!string.IsNullOrEmpty(OnClick))
            {
                output.Attributes.SetAttribute("onclick", OnClick);
            }
            output.Content.SetContent(ButtonText);
        }
    }
}
