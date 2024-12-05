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
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"CustomerAmount: {model.CustomerAmount[i]} PackagesAmount: {model.PackagesAmount[i]}");
            }

            List<Days> days = _daysRepository.getAllDaysOrdered();

            string prognosisId = model.prognosisId;
            int shelveMeters = _prognosisRepository.GetShelfMetersByPrognosis(prognosisId);

            int cassiereNorm = 30;
            int cassieresNeededForThirtyPerHour = norms[2].normInSeconds;
            int workersNorm = 100;
            int workersNeededForHundredPerHour = norms[3].normInSeconds;

            int colliUitladenNormInSeconds = norms[0].normInSeconds;
            int stockingNormInSeconds = norms[1].normInSeconds;
            int spiegelenNormInSeconds = norms[4].normInSeconds;

            List<int> cassiereHours = new List<int>();
            List<int> cassieresNeeded = new List<int>();

            List<int> versWorkersHours = new List<int>();
            List<int> workersNeeded = new List<int>();

            List<int> stockingHours = new List<int>();
            List<int> stockingWorkers = new List<int>();

            for (int i = 0; i < days.Count; i++)
            {
                Days day = days[i];
                int weekhours = day.Name.Equals("Zondag", StringComparison.OrdinalIgnoreCase) ? 8 : 13;

                if (i < model.CustomerAmount.Count)
                {
                    int customerAmount = model.CustomerAmount[i];

                    int cassiereHoursNeeded = customerAmount / cassiereNorm;
                    int workerHoursNeeded = customerAmount / workersNorm;

                    cassiereHours.Add(cassiereHoursNeeded);
                    cassieresNeeded.Add((cassiereHoursNeeded + 7) / 8);

                    versWorkersHours.Add(workerHoursNeeded);
                    workersNeeded.Add((workerHoursNeeded + 7) / 8);
                }

                if (i < model.PackagesAmount.Count)
                {
                    int packageAmount = model.PackagesAmount[i];

                    int colliUitladenHoursNeeded = (packageAmount * colliUitladenNormInSeconds) / 60;
                    int stockingHoursNeeded = (packageAmount * stockingNormInSeconds) / 60;
                    int spiegelenHoursNeeded = (shelveMeters * spiegelenNormInSeconds) / 3600;
                    int totalForStocking = (colliUitladenHoursNeeded + stockingHoursNeeded + spiegelenHoursNeeded);

                    totalForStocking /= 10;

                    stockingHours.Add(totalForStocking);
                    stockingWorkers.Add((totalForStocking + 7) / 8);
                }
            }

            CalculateViewmodel viewmodel = new CalculateViewmodel
            {
                PrognosisId = prognosisId,
                Days = days,
                CassiereHours = cassiereHours,
                VersWorkersHours = versWorkersHours,
                StockingHours = stockingHours,
                CassieresNeeded = cassieresNeeded,
                VersWorkersNeeded = workersNeeded,
                StockingWorkersNeeded = stockingWorkers
            };

            return viewmodel;
        }

    }
}
