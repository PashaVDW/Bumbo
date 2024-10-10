using Microsoft.AspNetCore.Mvc;
using bumbo.Components;

namespace bumbo.Controllers
{
    public class NormeringController : Controller
    {
        public class Normering
        {
            public string Week { get; set; }
            public int ColiUitladenMinuten { get; set; }
            public int VakkenVullenMinutenPerColi { get; set; }
            public string Kassa { get; set; }
            public string Vers { get; set; }
            public int SpiegelenSecondenPerMeter { get; set; }
        }

        public IActionResult Index(string searchTerm)
        {
            var headers = new List<string> { "Week", "Coli uitladen", "Vakken vullen", "Kassa", "Vers", "Spiegelen" };
            var normeringen = new List<Normering>
            {
                new Normering { Week = "Week 1", ColiUitladenMinuten = 5, VakkenVullenMinutenPerColi = 30, Kassa = "1 Kassière 30 klanten per uur", Vers = "1 medewerker per 100 klanten per uur", SpiegelenSecondenPerMeter = 30 },
                new Normering { Week = "Week 2", ColiUitladenMinuten = 6, VakkenVullenMinutenPerColi = 32, Kassa = "1 Kassière 25 klanten per uur", Vers = "1 medewerker per 80 klanten per uur", SpiegelenSecondenPerMeter = 35 },
            };

            var tableBuilder = new TableHtmlBuilder<Normering>();
            var htmlTable = tableBuilder.GenerateTable("Normeringen", headers, normeringen, item =>
            {
                return $@"
                <td class='py-2 px-4'>{item.Week}</td>
                <td class='py-2 px-4'>{item.ColiUitladenMinuten} minuten</td>
                <td class='py-2 px-4'>{item.VakkenVullenMinutenPerColi} minuten per coli</td>
                <td class='py-2 px-4'>{item.Kassa}</td>
                <td class='py-2 px-4'>{item.Vers}</td>
                <td class='py-2 px-4'>{item.SpiegelenSecondenPerMeter} seconden per meter</td>";
            }, searchTerm);

            ViewBag.HtmlTable = htmlTable;

            return View();
        }
    }
}
