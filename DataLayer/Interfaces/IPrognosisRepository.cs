using bumbo.Models;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Interfaces
{
    public interface IPrognosisRepository
    {
        List<Prognosis> GetAllPrognosis();
        Prognosis GetPrognosisByWeekAndYear(int weekNumber, int year);
        Prognosis GetLatestPrognosis();
        List<PrognosisHasDaysHasDepartment> GetPrognosisDetailsByBranchWeekAndYear(int branchId, int weekNumber, int year);
    }
}
