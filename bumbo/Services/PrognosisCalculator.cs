using bumbo.Models;
using bumbo.ViewModels.Prognosis;
using DataLayer.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public CalculateViewmodel CalculatePrognosis(InputCalculateViewModel model, List<Norm> norms)
        {
            List<Days> days = _daysRepository.getAllDaysOrdered();

            string prognosisId = model.prognosisId;
            int shelveMeters = _prognosisRepository.GetShelfMetersByPrognosis(prognosisId);

            int cassiereNorm = 30;
            int workersNorm = 100;

            int colliUitladenNormInSeconds = norms[0].normInSeconds;
            int stockingNormInSeconds = norms[1].normInSeconds;
            int spiegelenNormInSeconds = norms[4].normInSeconds;

            List<double> cassiereHours = new List<double>();
            List<int> cassieresNeeded = new List<int>();

            List<double> versWorkersHours = new List<double>();
            List<int> workersNeeded = new List<int>();

            List<double> stockingHours = new List<double>();
            List<int> stockingWorkers = new List<int>();

            for (int i = 0; i < days.Count; i++)
            {
                Days day = days[i];
                int weekhours = day.Name.Equals("Zondag", StringComparison.OrdinalIgnoreCase) ? 8 : 13;

                if (i < model.CustomerAmount.Count)
                {
                    int customerAmount = model.CustomerAmount[i];

                    // Calculate hours needed for cashiers and workers with decimals
                    double cassiereHoursNeeded = (double)customerAmount / cassiereNorm;
                    double workerHoursNeeded = (double)customerAmount / workersNorm;

                    cassiereHours.Add(Math.Round(cassiereHoursNeeded, 1)); // Round to 1 decimal place
                    cassieresNeeded.Add(Math.Max(1, (int)Math.Ceiling(cassiereHoursNeeded / 8)));

                    versWorkersHours.Add(Math.Round(workerHoursNeeded, 1)); // Round to 1 decimal place
                    workersNeeded.Add(Math.Max(1, (int)Math.Ceiling(workerHoursNeeded / 8)));
                }

                if (i < model.PackagesAmount.Count)
                {
                    int packageAmount = model.PackagesAmount[i];

                    double colliUitladenHoursNeeded = (double)(packageAmount * colliUitladenNormInSeconds) / 3600;
                    double stockingHoursNeeded = (double)(packageAmount * stockingNormInSeconds) / 3600;
                    double spiegelenHoursNeeded = (double)(shelveMeters * spiegelenNormInSeconds) / 3600;
                    double totalForStocking = (colliUitladenHoursNeeded + stockingHoursNeeded + spiegelenHoursNeeded);

                    stockingHours.Add(Math.Round(totalForStocking, 1)); // Round to 1 decimal place
                    stockingWorkers.Add(Math.Max(1, (int)Math.Ceiling(totalForStocking / 8)));
                }
            }

            CalculateViewmodel viewmodel = new CalculateViewmodel
            {
                PrognosisId = prognosisId,
                Days = days,
                CassiereHours = cassiereHours.Select(h => Math.Round(h, 1)).ToList(), // Ensure all are rounded
                VersWorkersHours = versWorkersHours.Select(h => Math.Round(h, 1)).ToList(),
                StockingHours = stockingHours.Select(h => Math.Round(h, 1)).ToList(),
                CassieresNeeded = cassieresNeeded,
                VersWorkersNeeded = workersNeeded,
                StockingWorkersNeeded = stockingWorkers
            };

            return viewmodel;
        }
    }
}
