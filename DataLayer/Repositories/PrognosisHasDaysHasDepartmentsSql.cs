using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
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

        public void createCalculation(
          int prognosisId,
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
                    var cassiereCalculation = new Prognosis_has_days_has_Department
                    {
                        PrognosisId = prognosisId,
                        Days_name = day.Name,
                        DepartmentName = "Kassa",
                        AmountWorkersNeeded = cassieresNeeded[day],
                        HoursWorkNeeded = cassiereHours[day]
                    };
                    _context.prognosis_Has_Days_Has_Departments.Add(cassiereCalculation);
                }

                if (versWorkersHours.ContainsKey(day))
                {
                    var versWorkersCalculation = new Prognosis_has_days_has_Department
                    {
                        PrognosisId = prognosisId,
                        Days_name = day.Name,
                        DepartmentName = "Vers",
                        AmountWorkersNeeded = workersNeeded[day],
                        HoursWorkNeeded = versWorkersHours[day]
                    };
                    _context.prognosis_Has_Days_Has_Departments.Add(versWorkersCalculation);
                }  

                if (stockingHours.ContainsKey(day))
                {
                    var stockingCalculation = new Prognosis_has_days_has_Department
                    {
                        PrognosisId = prognosisId,
                        Days_name = day.Name,
                        DepartmentName = "Vakkenvullen",
                        AmountWorkersNeeded = stockingHours[day] / divisor,
                        HoursWorkNeeded = stockingHours[day]
                    };
                    _context.prognosis_Has_Days_Has_Departments.Add(stockingCalculation);
                }
            }

            _context.SaveChanges();
        }
    }
}
