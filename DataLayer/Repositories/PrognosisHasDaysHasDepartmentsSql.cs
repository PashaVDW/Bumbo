﻿using bumbo.Data;
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
                    var cassiereCalculation = new PrognosisHasDaysHasDepartment
                    {
                        PrognosisId = prognosisId,
                        Days_name = day.Name,
                        DepartmentName = "Kassa",
                        AmountWorkersNeeded = cassieresNeeded[day],
                        HoursWorkNeeded = cassiereHours[day]
                    };
                    _context.PrognosisHasDaysHasDepartments.Add(cassiereCalculation);
                }

                if (versWorkersHours.ContainsKey(day))
                {
                    var versWorkersCalculation = new PrognosisHasDaysHasDepartment
                    {
                        PrognosisId = prognosisId,
                        Days_name = day.Name,
                        DepartmentName = "Vers",
                        AmountWorkersNeeded = workersNeeded[day],
                        HoursWorkNeeded = versWorkersHours[day]
                    };
                    _context.PrognosisHasDaysHasDepartments.Add(versWorkersCalculation);
                }  

                if (stockingHours.ContainsKey(day))
                {
                    var stockingCalculation = new PrognosisHasDaysHasDepartment
                    {
                        PrognosisId = prognosisId,
                        Days_name = day.Name,
                        DepartmentName = "Vakkenvullen",
                        AmountWorkersNeeded = stockingHours[day] / divisor,
                        HoursWorkNeeded = stockingHours[day]
                    };
                    _context.PrognosisHasDaysHasDepartments.Add(stockingCalculation);
                }
            }

            _context.SaveChanges();
        }

        public List<PrognosisHasDaysHasDepartment> GetPrognosisCalculations(int prognosisId)
        {
            return _context.PrognosisHasDaysHasDepartments
                      .Where(p => p.PrognosisId == prognosisId)
                      .ToList();
        }

        public void UpdateCalculations(List<PrognosisHasDaysHasDepartment> viewmodels)
        {
            _context.PrognosisHasDaysHasDepartments.UpdateRange(viewmodels);
            _context.SaveChanges();
        }
    }
}
