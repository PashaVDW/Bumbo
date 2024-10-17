using Microsoft.AspNetCore.Razor.TagHelpers;

namespace bumbo.Components
{
    [HtmlTargetElement("custom-modal")]
    public class CustomModal : TagHelper
    {

        public string ModalId { get; set; }
        public string Title { get; set; } = "Weet je het zeker?";
        public string BodyText { get; set; }
        public string ConfirmButtonText { get; set; } = "Ja";
        public string CancelButtonText { get; set; } = "Nee";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("id", ModalId);
            output.Attributes.SetAttribute("class", "fixed inset-0 z-50 flex items-center justify-center hidden bg-gray-800 bg-opacity-50");

            output.PreContent.SetHtmlContent($@"
                <div class='relative max-w-md w-full max-h-full p-4'>
                    <div class='relative bg-white rounded-lg shadow'>
                        <button onclick=""document.getElementById('{ModalId}').classList.add('hidden')"" class='absolute top-3 right-2.5 text-gray-400 bg-transparent w-8 h-8 inline-flex items-center justify-center rounded-lg text-sm hover:bg-gray-200 hover:text-gray-900'>
                            <svg class='w-3 h-3' xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 14 14'>
                                <path stroke='currentColor' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6' />
                            </svg>
                        </button>
                        <div class='p-4 text-center md:p-5'>
                            <svg class='mb-4 text-gray-400 w-12 h-12 mx-auto' xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 20 20'>
                                <path stroke='currentColor' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M10 11V6m0 8h.01M19 10a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z' />
                            </svg>
                            <h3 class='mb-5 text-lg font-normal text-gray-500'>{BodyText}</h3>
            ");

            output.PostContent.SetHtmlContent("</div></div></div>");
        }
    }
}
