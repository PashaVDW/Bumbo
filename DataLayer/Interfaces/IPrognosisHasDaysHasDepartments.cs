using bumbo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IPrognosisHasDaysHasDepartments
    {
        void createCalculation(int prognosisId, Dictionary<Days, int> cassiereHours, Dictionary<Days, int> versWorkersHours, Dictionary<Days, int> colliUitLadenMinutes, Dictionary<Days, int> stockingMinutes, Dictionary<Days, int> spiegelenMinutes);
    }
}
