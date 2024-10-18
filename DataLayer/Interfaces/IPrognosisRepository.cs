using bumbo.Models;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface IPrognosisRepository
    {

        Task<Prognosis> GetPrognosisForCurrentWeekAsync(int branchId);
        void AddPrognosis(List<Days> days, List<int> CustomerAmount, List<int> PackagesAmount, int week, int year);
    }
}
