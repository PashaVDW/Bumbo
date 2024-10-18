using bumbo.Models;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface IPrognosisRepository
    {
        List<Prognosis> GetAllPrognosis();
        Prognosis GetPrognosisByWeekAndYear(int weekNumber, int year);
        Prognosis GetLatestPrognosis();

        Task<Prognosis> GetPrognosisForCurrentWeekAsync(int branchId);
        void AddPrognosis(List<Days> days, List<int> CustomerAmount, List<int> PackagesAmount, int week, int year);
    }
}
