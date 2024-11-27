using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ILabourRulesRepository
    {
        void CreateDefaultLabourRulesForCountry(string activeCountry);
        List<LabourRules> GetAllLabourRulesForCountry(string countryName);
        LabourRules GetLabourRuleByCountyAndAgeGroup(string countryName, string ageGroup);
        void UpdateLabourRule(
           string ageGroup,
           string countryName,
           int maxHoursPerDay,
           TimeSpan maxEndTime,
           int maxHoursPerWeek,
           int maxWorkDaysPerWeek,
           int minRestDaysPerWeek,
           decimal numHoursWorkedBeforeBreak,
           decimal sickPayPercentage,
           decimal overtimePayPercentage,
           int minutesOfBreak,
           int maxHoursWithSchool,
           int minRestHoursBetweenShifts,
           int maxShiftDuration,
           int maxOvertimeHoursPerWeek
       );
    }
}
