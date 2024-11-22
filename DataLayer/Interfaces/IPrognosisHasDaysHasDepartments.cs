﻿using bumbo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IPrognosisHasDaysHasDepartments
    {
        void createCalculation(int prognosisId, Dictionary<Days, int> cassiereHours, Dictionary<Days, int> versWorkersHours, Dictionary<Days, int> stockingHours, Dictionary<Days, int> cassieresNeeded, Dictionary<Days, int> workersNeeded);
    }
}