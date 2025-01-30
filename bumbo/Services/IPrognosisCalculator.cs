using bumbo.Models;
using bumbo.ViewModels.Prognosis;

namespace bumbo.Services
{
    public interface IPrognosisCalculator
    {
        CalculateViewmodel CalculatePrognosis(InputCalculateViewModel model, List<Norm> norms);
    }
}