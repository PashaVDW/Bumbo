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

        public void CreateCalculation(string prognosisId, List<Days> days, List<int> cassiereHours, List<int> versWorkersHours, List<int> stockingHours, List<int> cassieresNeeded, List<int> workersNeeded, List<int> stockingWorkersNeeded)
        {
            for (int index = 0; index < days.Count; index++)
            {
                var cassiereCalculation = new PrognosisHasDaysHasDepartment
                {
                    PrognosisId = prognosisId,
                    DaysName = days[index].Name,
                    DepartmentName = "Kassa",
                    AmountOfWorkersNeeded = cassieresNeeded[index],
                    HoursOfWorkNeeded = cassiereHours[index]
                };
                _context.PrognosisHasDaysHasDepartment.Add(cassiereCalculation);

                var versWorkersCalculation = new PrognosisHasDaysHasDepartment
                {
                    PrognosisId = prognosisId,
                    DaysName = days[index].Name,
                    DepartmentName = "Vers",
                    AmountOfWorkersNeeded = workersNeeded[index],
                    HoursOfWorkNeeded = versWorkersHours[index]
                };
                _context.PrognosisHasDaysHasDepartment.Add(versWorkersCalculation);

                var stockingCalculation = new PrognosisHasDaysHasDepartment
                {
                    PrognosisId = prognosisId,
                    DaysName = days[index].Name,
                    DepartmentName = "Vakkenvullen",
                    AmountOfWorkersNeeded = stockingWorkersNeeded[index],
                    HoursOfWorkNeeded = stockingHours[index]
                };
                _context.PrognosisHasDaysHasDepartment.Add(stockingCalculation);

            }
            _context.SaveChanges();
        }

        public List<PrognosisHasDaysHasDepartment> GetPrognosisCalculations(string prognosisId)
        {
            return _context.PrognosisHasDaysHasDepartment
                      .Where(p => p.PrognosisId == prognosisId)
                      .ToList();
        }

        public void UpdatePrognosisDepartments(List<PrognosisHasDaysHasDepartment> departments)
        {
            _context.PrognosisHasDaysHasDepartment.UpdateRange(departments);
            _context.SaveChanges();
        }
    }
}
