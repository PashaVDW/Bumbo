using Microsoft.AspNetCore.Razor.TagHelpers;

namespace bumbo.Components
{
    [HtmlTargetElement("default-button")]
    public class CustomButton : TagHelper
    {
        public string ButtonText { get; set; } = "klik hier";
        /// <summary>
        /// Options: primary, secondary, delete, light, succes
        /// This will impact the styling, You also can add a option below.
        /// </summary>
        public string ButtonType { get; set; }
        public string OnClick { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            string defaultStyle = " text-white font-semibold py-2 px-6 rounded-xl ";
            string buttonClass = ButtonType?.ToLower() switch
            {
                "primary" => "primary_bg_color hover:bg-yellow-400" + defaultStyle + CustomStyle,
                "secondary" => "bg-gray-600 hover:bg-gray-500" + defaultStyle + CustomStyle,
                "delete" => "bg-red-600 hover:bg-red-500" + defaultStyle + CustomStyle,
                "light" => "bg-gray-400 hover:bg-gray-300" + defaultStyle + CustomStyle,
                "succes" => "bg-green-500 hover:bg-green-400" + defaultStyle + CustomStyle,
                _ => "primary_bg_color hover:bg-yellow-400" + defaultStyle + CustomStyle
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
