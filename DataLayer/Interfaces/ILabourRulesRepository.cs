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
        void UpdateLabourRule(LabourRules labourRule);
    }
}
