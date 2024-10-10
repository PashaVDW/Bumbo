namespace bumbo.Components
{
    using System.Text;
    using System.Linq;

    public class TableHtmlBuilder<TItem>
    {
        public string GenerateTable(string title, List<string> headers, List<TItem> items, Func<TItem, string> rowTemplate, string searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                items = items.Where(item =>
                {
                    var properties = typeof(TItem).GetProperties();
                    return properties.Any(prop => prop.GetValue(item)?.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true);
                }).ToList();
            }
            var htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<div class='container mx-auto'>" +
                                   "<div class='flex justify-between items-center mb-4'>" +
                                   "<h2>" + title + "</h2>" +
                                   "<form method='get' class='flex items-center space-x-4'>" +
                                   "<input type='text' name='searchTerm' value='" + searchTerm + "' placeholder='Zoek naar een weeknummer' class='w-full border border-gray-300 rounded-full py-2 px-6 focus:outline-none focus:ring-2 focus:ring-yellow-400' />" +
                                   "<button type='submit' class='bg-gray-200 text-gray-700 py-2 px-6 rounded-full hover:bg-gray-300'>Zoeken</button>" +
                                   "</form>" +
                                   "<button class='bg-gray-200 text-gray-700 py-2 px-4 rounded-md hover:bg-gray-300'>Nieuwe normering</button>" +
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
            foreach (var item in items)
            {
                htmlBuilder.AppendLine("<tr class='border-b hover:bg-gray-50'>");
                htmlBuilder.AppendLine(rowTemplate(item));
                htmlBuilder.AppendLine(
                    "<td class='py-2 px-4'>" +
                    "<button onclick=''>✏️</button>" +
                    "</td>"
                );
                htmlBuilder.AppendLine("</tr>");
            }
            htmlBuilder.AppendLine("</tbody>");
            htmlBuilder.AppendLine(
                "</table>" +
                "</div>" +
                "</div>" +
                "</div>"
                );
            return htmlBuilder.ToString();
        }
    }
}
