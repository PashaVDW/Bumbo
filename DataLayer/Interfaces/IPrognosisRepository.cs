using bumbo.Models;

namespace DataLayer.Interfaces
{
    public interface IPrognosisRepository
    {
        List<Prognosis> GetAllPrognosis();
        Prognosis GetLatestPrognosis();
    }
}
