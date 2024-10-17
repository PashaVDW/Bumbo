using Microsoft.AspNetCore.Mvc;
using bumbo.Components;
using bumbo.Models;

namespace bumbo.Controllers
{
    public class Medewerker
    {
        public int MedewerkerId { get; set; }
        public string Naam { get; set; }
        public string Functie { get; set; }
        public int BranchId { get; set; }
    }

    public class Branch
    {
        public int BranchId { get; set; }
        public string BranchNaam { get; set; }
    }

    public class MedewerkerHasBranch
    {
        public int MedewerkerId { get; set; }
        public int BranchId { get; set; }
    }

    public class MedewerkerController : Controller
    {
        public IActionResult Index(string searchTerm, int page = 1)
        {
            var medewerkers = new List<Medewerker>
            {
                new Medewerker { MedewerkerId = 1, Naam = "John Doe", Functie = "Verkoper", BranchId = 1 },
                new Medewerker { MedewerkerId = 2, Naam = "Jane Smith", Functie = "Manager", BranchId = 2 },
                new Medewerker { MedewerkerId = 3, Naam = "Sara Jones", Functie = "Kassamedewerker", BranchId = 1 },
                new Medewerker { MedewerkerId = 4, Naam = "Peter White", Functie = "Assistent", BranchId = 3 }
            };

            var branches = new List<Branch>
            {
                new Branch { BranchId = 1, BranchNaam = "Filiaal A" },
                new Branch { BranchId = 2, BranchNaam = "Filiaal B" },
                new Branch { BranchId = 3, BranchNaam = "Filiaal C" }
            };

            var medewerkerHasBranch = new List<MedewerkerHasBranch>
            {
                new MedewerkerHasBranch { MedewerkerId = 1, BranchId = 1 },
                new MedewerkerHasBranch { MedewerkerId = 2, BranchId = 2 },
                new MedewerkerHasBranch { MedewerkerId = 3, BranchId = 1 },
                new MedewerkerHasBranch { MedewerkerId = 4, BranchId = 3 },
                new MedewerkerHasBranch { MedewerkerId = 1, BranchId = 3 }
            };

            var headers = new List<string> { "Naam", "Functie", "Filiaal" };
            var tableBuilder = new TableHtmlBuilder<Medewerker>();
            var htmlTable = tableBuilder.GenerateTable("Medewerkers", headers, medewerkers, "", "../#edit", item =>
            {
                var branchNaam = branches.FirstOrDefault(b => b.BranchId == item.BranchId)?.BranchNaam ?? "Onbekend";

                return $@"
                    <td class='py-2 px-4'>{item.Naam}</td>
                    <td class='py-2 px-4'>{item.Functie}</td>
                    <td class='py-2 px-4'>{item.BranchId}</td>
                    <td class='py-2 px-4'>{branchNaam}</td>";
            }, searchTerm, page, branchId: 2);

            ViewBag.HtmlTable = htmlTable;

            return View();
        }
    }
}
