namespace DataLayer.Interfaces
{
    public interface IPrognosisHasDaysRepository
    {
        List<Prognosis_has_days> GetPrognosisHasDays(); // Ensure this is declared
        List<Prognosis_has_days> GetLatestPrognosis_has_days(); // Ensure this is declared
        List<Prognosis_has_days> GetPrognosisHasDaysByPrognosisId(int prognosisId); // Ensure this is declared
        void UpdatePrognosisHasDays(List<Prognosis_has_days> prognosisDayList); // Updating junktion table
    }
}
