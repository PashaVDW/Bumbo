using bumbo.Models;

namespace DataLayer.Interfaces
{
    public interface IPrognosisRepository
    {
        List<Prognosis> GetAllPrognosis();
        Prognosis GetPrognosisByWeekAndYear(int weekNumber, int year);
        Prognosis GetLatestPrognosis();
        void AddPrognosis(List<Days> days, List<int> CustomerAmount, List<int> PackagesAmount, int week, int year);
        Prognosis GetPrognosisById(int id); // New method
        void UpdatePrognosis(int prognosisId, List<int> CustomerAmount, List<int> PackagesAmount); // Existing method
    }
}
