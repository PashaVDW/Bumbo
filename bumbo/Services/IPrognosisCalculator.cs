using bumbo.ViewModels.Prognosis;

namespace bumbo.Services
{
    public interface IPrognosisCalculator
    {
        CalculateViewmodel CalculatePrognosis(InputCalculateViewModel model);
    }
}