namespace bumbo.Components
{
    using System.Text;
    using System.Linq;

    public class TableHtmlBuilder<TItem>
    {
        public string GenerateTable(string title, List<string> headers, List<TItem> items, string addPageLink, Func<TItem, string> rowTemplate, string searchTerm = null, int currentPage = 1, int pageSize = 10)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                items = items.Where(item =>
                {
                    var properties = typeof(TItem).GetProperties();
                    return properties.Any(prop => prop.GetValue(item)?.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true);
                }).ToList();
            }

            var totalItems = items.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

            var pagedItems = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<div class='container mx-auto p-10'>" +
                                   "<div class='flex justify-between items-center mb-4'>" +
                                   "<h2 class='mb-4 text-4xl font-bold leading-none tracking-tight text-gray-900'>" + title + "</h2>" +
                                   "<form method='get' class='flex items-center space-x-4'>" +
                                   "<input type='text' name='searchTerm' value='" + searchTerm + "' placeholder='Zoek naar " + title.ToLower() + "' class='w-full border border-gray-300 rounded-full py-2 px-6 focus:outline-none focus:ring-2 focus:ring-yellow-400' />" +
                                   "<button type='submit' class='bg-gray-200 text-gray-700 py-2 px-6 rounded-full hover:bg-gray-300'>Zoeken</button>" +
                                   "</form>" +
                                   "<button onclick = \"window.location.href='" + addPageLink + "';\" class='bg-gray-600 hover:bg-gray-500 text-white font-semibold py-2 px-6 rounded-xl '>Nieuwe " + title.ToLower() + " </button>" +
                                   "</div>"
                                   );
            htmlBuilder.AppendLine("<div class='w-full p-6'>");
            htmlBuilder.AppendLine("<div class='overflow-x-auto w-full'>");
            htmlBuilder.AppendLine("<table class='min-w-full table-auto border-collapse'>");

            htmlBuilder.AppendLine(
                "<thead>" +
                "<tr class='text-left text-gray-600 font-bold'>"
            );
            foreach (var header in headers)
            {
                htmlBuilder.AppendLine($"<th class='py-2 px-4'>{header}</th>");
            }
            htmlBuilder.AppendLine(
                "</tr>" +
                "</thead>"
            );
            htmlBuilder.AppendLine("<tbody>");
            foreach (var item in pagedItems)
            {
                htmlBuilder.AppendLine("<tr class='border-b hover:bg-gray-50'>");
                htmlBuilder.AppendLine(rowTemplate(item));
                htmlBuilder.AppendLine("</tr>");
            }
            htmlBuilder.AppendLine("</tbody>");
            htmlBuilder.AppendLine("</table>");
            htmlBuilder.AppendLine("</div>");

            htmlBuilder.AppendLine("<div class='flex justify-center items-center mt-4 space-x-2'>");

            if (currentPage > 1)
            {
                htmlBuilder.AppendLine($"<a href='?page={currentPage - 1}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 rounded-lg hover:bg-gray-300'>Vorige</a>");
            }
            else
            {
                htmlBuilder.AppendLine("<span class='py-2 px-4 bg-gray-100 text-gray-400 rounded-lg'>Vorige</span>");
            }

            for (int i = 1; i <= totalPages; i++)
            {
                if (i == currentPage)
                {
                    htmlBuilder.AppendLine($"<span class='py-2 px-4 bg-yellow-400 text-white rounded-lg'>{i}</span>");
                }
                else
                {
                    htmlBuilder.AppendLine($"<a href='?page={i}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 text-gray-700 rounded-lg hover:bg-gray-300'>{i}</a>");
                }
            }
            if (currentPage < totalPages)
            {
                htmlBuilder.AppendLine($"<a href='?page={currentPage + 1}&searchTerm={searchTerm}' class='py-2 px-4 bg-gray-200 rounded-lg hover:bg-gray-300'>Volgende</a>");
            }
            else
            {
                htmlBuilder.AppendLine("<span class='py-2 px-4 bg-gray-100 text-gray-400 rounded-lg'>Volgende</span>");
            }
            htmlBuilder.AppendLine("</div>");

            htmlBuilder.AppendLine("</div>");

            return htmlBuilder.ToString();
        }


    }
}