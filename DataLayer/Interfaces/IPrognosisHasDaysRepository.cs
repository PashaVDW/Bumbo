﻿using bumbo.Models;

namespace DataLayer.Interfaces
{
    public interface IPrognosisHasDaysRepository
    {
        List<PrognosisHasDays> GetPrognosisHasDays(); // Ensure this is declared
        List<PrognosisHasDays> GetLatestPrognosis_has_days(); // Ensure this is declared
        List<PrognosisHasDays> GetPrognosisHasDaysByPrognosisId(string prognosisId); // Ensure this is declared
        void UpdatePrognosisHasDays(List<PrognosisHasDays> prognosisDayList);
    }
}
