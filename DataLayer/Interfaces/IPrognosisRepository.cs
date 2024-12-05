using bumbo.Models;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Interfaces
{
    public interface IPrognosisRepository
    {
        List<Prognosis> GetAllPrognosis();
        Prognosis GetPrognosisByWeekAndYear(int weekNumber, int year);
        Prognosis GetLatestPrognosis(int value);
        List<PrognosisHasDaysHasDepartment> GetPrognosisDetailsByBranchWeekAndYear(int branchId, int weekNumber, int year);
        string AddPrognosis(List<Days> days, List<int> CustomerAmount, List<int> PackagesAmount, int week, int year, int managerOfBranchId);
        Prognosis GetPrognosisById(string branchId);
        void UpdatePrognosis(string prognosisId, List<int> CustomerAmount, List<int> PackagesAmount);
        void DeletePrognosisById(string id);
        int GetShelfMetersByPrognosis(string id);
    }
}
