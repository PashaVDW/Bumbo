using bumbo.Data;
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
    public class LabourRulesRepositorySql : ILabourRulesRepository
    {
        private readonly BumboDBContext _context;

        public LabourRulesRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<LabourRules> GetAllLabourRulesForCountry(string countryName)
        {
            return _context.LabourRules.Where(lr => lr.CountryName.Equals(countryName)).ToList();
        }
        public LabourRules GetLabourRuleByCountyAndAgeGroup(string countryName, string ageGroup)
        {
            if (string.IsNullOrEmpty(countryName) || string.IsNullOrEmpty(ageGroup))
            {
                throw new ArgumentException("County name or age group cannot be null or empty.");
            }
            return _context.LabourRules.FirstOrDefault(
                lr => lr.CountryName == countryName
                && lr.AgeGroup == ageGroup
            );
        }
        public List<LabourRules> CreateDefaultLabourRulesForCountry(string activeCountry)
        {
            var defaultLabourRules = new List<LabourRules>
            {
                new LabourRules
                {
                    CountryName = activeCountry,
                    AgeGroup = "<16",
                    MaxHoursPerDay = 8,
                    MaxEndTime = new TimeSpan(18, 0, 0),
                    MaxHoursPerWeek = 40,
                    MaxWorkDaysPerWeek = 5,
                    MaxHoursWithSchool = 12,
                    MinRestDaysPerWeek = 2,
                    NumHoursWorkedBeforeBreak = 4,
                    MinutesOfBreak = 30,
                    SickPayPercentage = 70m,
                    OvertimePayPercentage = 0m,
                    MinRestHoursBetweenShifts = 12,
                    MaxShiftDuration = 8,
                    MaxOvertimeHoursPerWeek = 0
                },
                new LabourRules
                {
                    CountryName = activeCountry,
                    AgeGroup = "16-17",
                    MaxHoursPerDay = 9,
                    MaxEndTime = new TimeSpan(22, 0, 0),
                    MaxHoursPerWeek = 40,
                    MaxWorkDaysPerWeek = 5,
                    MaxHoursWithSchool = 40,
                    MinRestDaysPerWeek = 2,
                    NumHoursWorkedBeforeBreak = 4,
                    MinutesOfBreak = 30,
                    SickPayPercentage = 70m,
                    OvertimePayPercentage = 0m,
                    MinRestHoursBetweenShifts = 12,
                    MaxShiftDuration = 9,
                    MaxOvertimeHoursPerWeek = 0
                },
                new LabourRules
                {
                    CountryName = activeCountry,
                    AgeGroup = ">17",
                    MaxHoursPerDay = 12,
                    MaxEndTime = new TimeSpan(23, 0, 0),
                    MaxHoursPerWeek = 60,
                    MaxWorkDaysPerWeek = 6,
                    MaxHoursWithSchool = 0,
                    MinRestDaysPerWeek = 1,
                    NumHoursWorkedBeforeBreak = 4,
                    MinutesOfBreak = 30,
                    SickPayPercentage = 70m,
                    OvertimePayPercentage = 100m,
                    MinRestHoursBetweenShifts = 11,
                    MaxShiftDuration = 12,
                    MaxOvertimeHoursPerWeek = 20
                }
            };
            _context.LabourRules.AddRange(defaultLabourRules);
            _context.SaveChanges();

            if (_context.LabourRules.Any(lr => lr.CountryName == activeCountry))
            {
                return _context.LabourRules.Where(lr => lr.CountryName == activeCountry).ToList();
            }
            else
            {
                throw new Exception("Failed to create default labour rules for country.");
            }
        }

        public void UpdateLabourRule(string ageGroup, string countryName, int maxHoursPerDay, TimeSpan maxEndTime, int maxHoursPerWeek, int maxWorkDaysPerWeek, int minRestDaysPerWeek, decimal numHoursWorkedBeforeBreak, decimal sickPayPercentage, decimal overtimePayPercentage, int minutesOfBreak, int maxHoursWithSchool, int minRestHoursBetweenShifts, int maxShiftDuration, int maxOvertimeHoursPerWeek)
        {
            LabourRules labourRules = _context.LabourRules.First(LabourRules => LabourRules.AgeGroup == ageGroup && LabourRules.CountryName == countryName);
            labourRules.MaxHoursPerDay = maxHoursPerDay;
            labourRules.MaxEndTime = maxEndTime;
            labourRules.MaxHoursPerWeek = maxHoursPerWeek;
            labourRules.MaxWorkDaysPerWeek = maxWorkDaysPerWeek;
            labourRules.MinRestDaysPerWeek = minRestDaysPerWeek;
            labourRules.NumHoursWorkedBeforeBreak = numHoursWorkedBeforeBreak;
            labourRules.SickPayPercentage = sickPayPercentage;
            labourRules.OvertimePayPercentage = overtimePayPercentage;
            labourRules.MinRestHoursBetweenShifts = minRestHoursBetweenShifts;
            labourRules.MinutesOfBreak = minutesOfBreak;
            labourRules.MaxHoursWithSchool = maxHoursWithSchool;
            labourRules.MaxShiftDuration = maxShiftDuration;
            labourRules.MaxOvertimeHoursPerWeek = maxOvertimeHoursPerWeek;
            _context.SaveChanges();
        }

    }
}
