using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface IPrognosisRepository
    {

        void getPrognosisForThisWeek();
        void AddPrognosis(List<Days> days, List<int> CustomerAmount, List<int> PackagesAmount, int week, int year);
    }
}
