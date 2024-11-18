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
          Dictionary<Days, int> colliUitLadenMinutes,
          Dictionary<Days, int> stockingMinutes,
          Dictionary<Days, int> spiegelenMinutes)
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
                        AmountWorkersNeeded = cassiereHours[day] / divisor,
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
                        AmountWorkersNeeded = versWorkersHours[day] / divisor,
                        HoursWorkNeeded = versWorkersHours[day]
                    };
                    _context.prognosis_Has_Days_Has_Departments.Add(versWorkersCalculation);
                }

                if (colliUitLadenMinutes.ContainsKey(day))
                {
                    var colliCalculation = new Prognosis_has_days_has_Department
                    {
                        PrognosisId = prognosisId,
                        Days_name = day.Name,
                        DepartmentName = "Coli uitladen",
                        AmountWorkersNeeded = colliUitLadenMinutes[day] / divisor,
                        HoursWorkNeeded = colliUitLadenMinutes[day]
                    };
                    _context.prognosis_Has_Days_Has_Departments.Add(colliCalculation);
                }

                if (stockingMinutes.ContainsKey(day))
                {
                    var stockingCalculation = new Prognosis_has_days_has_Department
                    {
                        PrognosisId = prognosisId,
                        Days_name = day.Name,
                        DepartmentName = "Vakkenvullen",
                        AmountWorkersNeeded = stockingMinutes[day] / divisor,
                        HoursWorkNeeded = stockingMinutes[day]
                    };
                    _context.prognosis_Has_Days_Has_Departments.Add(stockingCalculation);
                }

                if (spiegelenMinutes.ContainsKey(day))
                {
                    var spiegelenCalculation = new Prognosis_has_days_has_Department
                    {
                        PrognosisId = prognosisId,
                        Days_name = day.Name,
                        DepartmentName = "Spiegelen",
                        AmountWorkersNeeded = spiegelenMinutes[day] / divisor,
                        HoursWorkNeeded = spiegelenMinutes[day]
                    };
                    _context.prognosis_Has_Days_Has_Departments.Add(spiegelenCalculation);
                }
            }

            _context.SaveChanges();
        }
    }
}
