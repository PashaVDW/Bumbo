using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using bumbo.Models;
using DataLayer.Interfaces;

namespace bumbo.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        readonly IPrognosisHasDaysRepository _prognosisHasDaysRepository;
        readonly IPrognosisRepository _prognosisRepository;

        public ReviewsController(UserManager<Employee> userManager, IPrognosisRepository prognosisRepository, IPrognosisHasDaysRepository prognosisHasDaysRepository)
        {
            _userManager = userManager;
            _prognosisRepository = prognosisRepository;
            _prognosisHasDaysRepository = prognosisHasDaysRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null || user.ManagerOfBranchId == null)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            List<Prognosis> Prognosis = _prognosisRepository.GetAllPrognosis();
            List<Prognosis_has_days> prognosis_Has_Days = _prognosisHasDaysRepository.GetPrognosis_has_days();

            return View();
        }
    }
}
