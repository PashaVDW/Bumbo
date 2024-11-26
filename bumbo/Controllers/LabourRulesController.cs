using bumbo.ViewModels;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace bumbo.Controllers
{
    public class LabourRulesController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILabourRulesRepository _labourRulesRepository;
        public LabourRulesController(ICountryRepository countryRepository, ILabourRulesRepository labourRulesRepository)
        {
            _countryRepository = countryRepository;
            _labourRulesRepository = labourRulesRepository;
        }
        public IActionResult Index(string? activeCountry)
        {
            if (activeCountry == null)
            {
                activeCountry = "Netherlands";
            }
            var labourRules = _labourRulesRepository.GetAllLabourRulesForCountry(activeCountry).ToList();
            if (!labourRules.Any())
            {
                _labourRulesRepository.CreateDefaultLabourRulesForCountry(activeCountry);
            }
            var labourRulesViewModel = labourRules.Select(labourRule => new LabourRulesViewModel
            {
                AgeGroup = labourRule.AgeGroup,
                MaxHoursPerDay = labourRule.MaxHoursPerDay,
                MaxEndTime = labourRule.MaxEndTime,
                MaxHoursPerWeek = labourRule.MaxHoursPerWeek,
                MaxWorkDaysPerWeek = labourRule.MaxWorkDaysPerWeek,
                MinRestDaysPerWeek = labourRule.MinRestDaysPerWeek,
                NumHoursWorkedBeforeBreak = labourRule.NumHoursWorkedBeforeBreak,
                SickPayPercentage = labourRule.SickPayPercentage,
                OvertimePayPercentage = labourRule.OvertimePayPercentage,
                MinutesOfBreak = labourRule.MinutesOfBreak,
                MaxHoursWithSchool = labourRule.MaxHoursWithSchool,
                MinRestHoursBetweenShifts = labourRule.MinRestHoursBetweenShifts,
                MaxShiftDuration = labourRule.MaxShiftDuration,
                MaxOvertimeHoursPerWeek = labourRule.MaxOvertimeHoursPerWeek
            }).ToList();

            var groupedByProperties = labourRulesViewModel
                 .Where(lr => labourRules.All(r =>
                     r.MaxHoursPerDay == lr.MaxHoursPerDay &&
                     r.MaxEndTime == lr.MaxEndTime &&
                     r.MaxHoursPerWeek == lr.MaxHoursPerWeek &&
                     r.MaxWorkDaysPerWeek == lr.MaxWorkDaysPerWeek &&
                     r.MinRestDaysPerWeek == lr.MinRestDaysPerWeek &&
                     r.NumHoursWorkedBeforeBreak == lr.NumHoursWorkedBeforeBreak &&
                     r.SickPayPercentage == lr.SickPayPercentage &&
                     r.OvertimePayPercentage == lr.OvertimePayPercentage &&
                     r.MinutesOfBreak == lr.MinutesOfBreak &&
                     r.MaxHoursWithSchool == lr.MaxHoursWithSchool &&
                     r.MinRestHoursBetweenShifts == lr.MinRestHoursBetweenShifts &&
                     r.MaxShiftDuration == lr.MaxShiftDuration &&
                     r.MaxOvertimeHoursPerWeek == lr.MaxOvertimeHoursPerWeek
                 ))
                 .GroupBy(lr => new
                 {
                     lr.MaxHoursPerDay,
                     lr.MaxEndTime,
                     lr.MaxHoursPerWeek,
                     lr.MaxWorkDaysPerWeek,
                     lr.MinRestDaysPerWeek,
                     lr.NumHoursWorkedBeforeBreak,
                     lr.SickPayPercentage,
                     lr.OvertimePayPercentage,
                     lr.MinutesOfBreak,
                     lr.MaxHoursWithSchool,
                     lr.MinRestHoursBetweenShifts,
                     lr.MaxShiftDuration,
                     lr.MaxOvertimeHoursPerWeek
                 })
                 .Where(group => group.Count() == labourRules.Count())
                 .Select(group => group.ToList())
                 .ToList();
            var LabourRulesViewModel = new LabourRulesGroupViewModel
            {
                Countries = _countryRepository.GetCountries(),
                ActiveCountry = activeCountry,
                GeneralLabourRules = labourRulesViewModel,
                AgeLabourRules = labourRulesViewModel.Where(r => r.AgeGroup != null).ToList()
            };
            return View(LabourRulesViewModel);
        }
    }
}
