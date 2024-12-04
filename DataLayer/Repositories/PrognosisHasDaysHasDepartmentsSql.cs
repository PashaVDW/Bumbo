using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class PrognosisHasDaysHasDepartmentsSql : IPrognosisHasDaysHasDepartments
    {
        private readonly BumboDBContext _context;

        public PrognosisHasDaysHasDepartmentsSql(BumboDBContext context)
        {
            _context = context;
        }

        public void CreateCalculation(
          string prognosisId,
          Dictionary<Days, int> cassiereHours,
          Dictionary<Days, int> versWorkersHours,
          Dictionary<Days, int> stockingHours,
          Dictionary<Days, int> cassieresNeeded,
          Dictionary<Days, int> workersNeeded)
        {
            foreach (Days day in cassiereHours.Keys)
            {
                int divisor = day.Name.Equals("Zondag", StringComparison.OrdinalIgnoreCase) ? 8 : 13;

                if (cassiereHours.ContainsKey(day))
                {
                    var cassiereCalculation = new PrognosisHasDaysHasDepartment
                    {
                        PrognosisId = prognosisId,
                        DaysName = day.Name,
                        DepartmentName = "Kassa",
                        AmountOfWorkersNeeded = cassieresNeeded[day],
                        HoursOfWorkNeeded = cassiereHours[day]
                    };
                    _context.PrognosisHasDaysHasDepartment.Add(cassiereCalculation);
                }

                if (versWorkersHours.ContainsKey(day))
                {
                    var versWorkersCalculation = new PrognosisHasDaysHasDepartment
                    {
                        PrognosisId = prognosisId,
                        DaysName = day.Name,
                        DepartmentName = "Vers",
                        AmountOfWorkersNeeded = workersNeeded[day],
                        HoursOfWorkNeeded = versWorkersHours[day]
                    };
                    _context.PrognosisHasDaysHasDepartment.Add(versWorkersCalculation);
                }  

                if (stockingHours.ContainsKey(day))
                {
                    var stockingCalculation = new PrognosisHasDaysHasDepartment
                    {
                        PrognosisId = prognosisId,
                        DaysName = day.Name,
                        DepartmentName = "Vakkenvullen",
                        AmountOfWorkersNeeded = stockingHours[day] / divisor,
                        HoursOfWorkNeeded = stockingHours[day]
                    };
                    _context.PrognosisHasDaysHasDepartment.Add(stockingCalculation);
                }
            }

            _context.SaveChanges();
        }

        public List<PrognosisHasDaysHasDepartment> GetPrognosisCalculations(string prognosisId)
        {
            return _context.PrognosisHasDaysHasDepartment
                      .Where(p => p.PrognosisId == prognosisId)
                      .ToList();
        }

        public void UpdateCalculations(List<PrognosisHasDaysHasDepartment> viewmodels)
        {
            _context.PrognosisHasDaysHasDepartment.UpdateRange(viewmodels);
            _context.SaveChanges();
        }
    }
}
