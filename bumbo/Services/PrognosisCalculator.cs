using bumbo.Models;
using bumbo.ViewModels.Prognosis;
using DataLayer.Interfaces;

namespace bumbo.Services
{
    public class PrognosisCalculator : IPrognosisCalculator
    {
        private readonly IDaysRepositorySQL _daysRepository;
        private readonly INormsRepository _normsRepository;
        private readonly IPrognosisRepository _prognosisRepository;

        public PrognosisCalculator(IDaysRepositorySQL daysRepository, INormsRepository normsRepository, IPrognosisRepository prognosisRepository)
        {
            _daysRepository = daysRepository;
            _normsRepository = normsRepository;
            _prognosisRepository = prognosisRepository;
        }

        public CalculateViewmodel CalculatePrognosis(InputCalculateViewModel model)
        {
            List<Days> days = _daysRepository.getAllDaysOrdered();

            List<Norm> norms = _normsRepository.GetSelectedNorms(1, model.Year, model.WeekNr).Result;
            string prognosisId = _prognosisRepository.GetLatestPrognosis().PrognosisId;
            int shelveMeters = _prognosisRepository.GetShelfMetersByPrognosis(prognosisId);

            int cassiereNorm = 30;
            int cassieresNeededForThirtyPerHour = norms[2].normInSeconds;
            int workersNorm = 100;
            int workersNeededForHundredPerHour = norms[3].normInSeconds;

            int colliUitladenNormInSeconds = norms[0].normInSeconds;
            int stockingNormInSeconds = norms[1].normInSeconds;
            int spiegelenNormInSeconds = norms[4].normInSeconds;

            Dictionary<Days, int> cassiereHours = new Dictionary<Days, int>();
            Dictionary<Days, int> cassieresNeeded = new Dictionary<Days, int>();

            Dictionary<Days, int> versWorkersHours = new Dictionary<Days, int>();
            Dictionary<Days, int> workersNeeded = new Dictionary<Days, int>();

            Dictionary<Days, int> stockingHours = new Dictionary<Days, int>();

            for (int i = 0; i < days.Count; i++)
            {
                Days day = days[i];
                int weekhours = day.Name.Equals("Zondag", StringComparison.OrdinalIgnoreCase) ? 8 : 13;

                if (i < model.CustomerAmount.Count)
                {
                    int customerAmount = model.CustomerAmount[i];

                    int cassiereHoursNeeded = customerAmount / cassiereNorm;
                    int workerHoursNeeded = customerAmount / workersNorm;

                    cassieresNeeded.Add(day, (cassiereHoursNeeded * cassieresNeededForThirtyPerHour));
                    cassiereHours.Add(day, cassiereHoursNeeded);

                    versWorkersHours.Add(day, workerHoursNeeded);
                    workersNeeded.Add(day, (workerHoursNeeded * workersNeededForHundredPerHour));
                }

                if (i < model.PackagesAmount.Count)
                {
                    int packageAmount = model.PackagesAmount[i];

                    int colliUitladenHoursNeeded = (packageAmount * colliUitladenNormInSeconds) / 60;
                    int stockingHoursNeeded = (packageAmount * stockingNormInSeconds) / 60;
                    int spiegelenHoursNeeded = (shelveMeters * spiegelenNormInSeconds) / 3600;
                    int totalForStocking = (colliUitladenHoursNeeded + stockingHoursNeeded + spiegelenHoursNeeded);

                    stockingHours.Add(day, totalForStocking);
                }
            }

            CalculateViewmodel viewmodel = new CalculateViewmodel();

            viewmodel.PrognosisId = prognosisId;
            viewmodel.CassiereHours = cassiereHours;
            viewmodel.VersWorkersHours = versWorkersHours;
            viewmodel.StockingHours = stockingHours;
            viewmodel.CassieresNeeded = cassieresNeeded;
            viewmodel.WorkersNeeded = workersNeeded;

            return viewmodel;
        }
    }
}
