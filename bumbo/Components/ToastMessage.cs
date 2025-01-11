using Microsoft.AspNetCore.Razor.TagHelpers;

namespace bumbo.Components
{
    [HtmlTargetElement("toast-message")]
    public class ToastMessage : TagHelper
    {
        public string ToastId { get; set; }
        public string Message { get; set; } = "Dit is een toast!";
        /// <summary>
        ///  Options: info, success, error.
        ///  This inpacts the styling of the message.
        /// </summary>
        public string MessageType { get; set; } = "info";
        public string CustomStyle { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            string toastClass = MessageType.ToLower() switch
            {
                "success" => "bg-green-500 text-white",
                "error" => "bg-red-500 text-white",
                _ => "bg-green-500 text-white"
            };

            output.Attributes.SetAttribute("id", ToastId);
            output.Attributes.SetAttribute("class", $"fixed bottom-5 right-5 z-50 p-4 rounded-lg shadow-md {toastClass} {CustomStyle}");
            output.Attributes.SetAttribute("style", "display: none;");

            string toastHtml = $@"
                <div class='flex items-center justify-between'>
                    <span>{Message}</span>
                    <button onclick=""document.getElementById('{ToastId}').style.display='none'"" class='ml-4 bg-transparent text-white'>
                        <svg xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 24 24' stroke='currentColor' class='w-4 h-4'>
                            <path stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M6 18L18 6M6 6l12 12' />
                        </svg>
                    </button>
                </div>";

            output.Content.SetHtmlContent(toastHtml);

            output.PostContent.AppendHtml(@"
                <script>
                    function showToast(toastId, autoHide, delay) {
                        var toast = document.getElementById(toastId);
                        toast.style.display = 'block';

                        // Check of auto-hide is ingeschakeld
                        if (autoHide.toLowerCase() === 'yes') {
                            setTimeout(function() {
                                toast.style.display = 'none';
                            }, delay);
                        }
                    }
                </script>");
        }
    }
}
