using bumbo.Models;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IPrognosisHasDaysHasDepartments
    {
        void CreateCalculation(string prognosisId, List<Days> days, List<int> cassiereHours, List<int> versWorkersHours, List<int> stockingHours, List<int> cassieresNeeded, List<int> workersNeeded, List<int> stockingWorkersNeeded);
        List<PrognosisHasDaysHasDepartment> GetPrognosisCalculations(string prognosisId);
        void UpdateCalculations(List<PrognosisHasDaysHasDepartment> viewmodels);
    }
}
