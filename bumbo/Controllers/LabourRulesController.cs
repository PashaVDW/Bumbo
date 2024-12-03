using bumbo.ViewModels;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace bumbo.Controllers
{
    public class LabourRulesController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILabourRulesRepository _labourRulesRepository;
        private readonly UserManager<Employee> _userManager;
        public LabourRulesController(ICountryRepository countryRepository, ILabourRulesRepository labourRulesRepository, UserManager<Employee> userManager)
        {
            _countryRepository = countryRepository;
            _labourRulesRepository = labourRulesRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync(string? activeCountry)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null || !currentUser.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            if (activeCountry == null)
            {
                activeCountry = "Netherlands";
            }
            var labourRules = _labourRulesRepository.GetAllLabourRulesForCountry(activeCountry).ToList();
            if (!labourRules.Any())
            {
                labourRules = _labourRulesRepository.CreateDefaultLabourRulesForCountry(activeCountry);
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
            var LabourRulesViewModel = new LabourRulesGroupViewModel
            {
                Countries = _countryRepository.GetCountries(),
                ActiveCountry = activeCountry,
                LabourRules = labourRulesViewModel
            };
            return View(LabourRulesViewModel);
        }
        public async Task<IActionResult> EditAsync(string countryName, string ageGroup)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null || !currentUser.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            var labourRule = _labourRulesRepository.GetLabourRuleByCountyAndAgeGroup(countryName, ageGroup);
            var labourRulesViewModel = new LabourRulesViewModel
            {
                AgeGroup = labourRule.AgeGroup,
                CountryName = labourRule.CountryName,
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
            };
            return View(labourRulesViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> HandleEditAsync(LabourRulesViewModel labourRulesViewModel)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null || !currentUser.IsSystemManager)
            {
                return RedirectToAction("AccessDenied", "Home");
            }
            _labourRulesRepository.UpdateLabourRule(
                labourRulesViewModel.AgeGroup,
                labourRulesViewModel.CountryName,
                labourRulesViewModel.MaxHoursPerDay,
                labourRulesViewModel.MaxEndTime,
                labourRulesViewModel.MaxHoursPerWeek,
                labourRulesViewModel.MaxWorkDaysPerWeek,
                labourRulesViewModel.MinRestDaysPerWeek,
                labourRulesViewModel.NumHoursWorkedBeforeBreak,
                labourRulesViewModel.SickPayPercentage,
                labourRulesViewModel.OvertimePayPercentage,
                labourRulesViewModel.MinutesOfBreak,
                labourRulesViewModel.MaxHoursWithSchool,
                labourRulesViewModel.MinRestHoursBetweenShifts,
                labourRulesViewModel.MaxShiftDuration,
                labourRulesViewModel.MaxOvertimeHoursPerWeek
                );
            return RedirectToAction("Index", new { activeCountry = labourRulesViewModel.CountryName });
        }
    }
}
