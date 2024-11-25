﻿using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DataLayer.Repositories
{
    public class PrognosisRepositorySql : IPrognosisRepository
    {
        readonly BumboDBContext _context;

        public PrognosisRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public void AddPrognosis(List<Days> days, List<int> CustomerAmount, List<int> PackagesAmount, int week, int year)
        {
            var prognosis = new Prognosis
            {
                BranchId = 1,
                WeekNr = week,
                Year = year
            };

            _context.Prognoses.Add(prognosis);
            _context.SaveChanges();

            if (days.Count == CustomerAmount.Count && days.Count == PackagesAmount.Count)
            {
                foreach (var day in days)
                {
                    if (!_context.Days.Any(d => d.Name == day.Name))
                    {
                        throw new InvalidOperationException($"Day '{day.Name}' does not exist in the Days table.");
                    }

                    var prognosisHasDays = new Prognosis_has_days
                    {
                        Days_name = day.Name,
                        PrognosisId = prognosis.PrognosisId,
                        CustomerAmount = CustomerAmount[days.IndexOf(day)],
                        PackagesAmount = PackagesAmount[days.IndexOf(day)]
                    };

                    _context.Prognosis_Has_Days.Add(prognosisHasDays);
                    _context.SaveChanges();
                }
            }
            else
            {
                throw new ArgumentException("Days, CustomerAmount, and PackagesAmount lists must have the same number of elements.");
            }
        }

        public List<Prognosis> GetAllPrognosis()
        {
            return _context.Prognoses.ToList();
        }

        public Prognosis GetLatestPrognosis()
        {
            List<Prognosis> prognosis = _context.Prognoses
                .Include(p => p.PrognosisHasDays)
                    .ThenInclude(phd => phd.PrognosisHasDaysHasDepartment)
                .OrderByDescending(p => p.Year)
                .ThenByDescending(p => p.WeekNr)
                .ToList<Prognosis>();
            return prognosis.First();
        }

        public Prognosis GetPrognosisByWeekAndYear(int weekNumber, int year)
        {
            List<Prognosis> prognosis = _context.Prognoses
                .Include(p => p.PrognosisHasDays)
                    .ThenInclude(phd => phd.PrognosisHasDaysHasDepartment)
                .Where(p => p.WeekNr == weekNumber && p.Year == year)
                .ToList();

            return prognosis.FirstOrDefault();
        }

        public void UpdatePrognosis(int prognosisId, List<int> CustomerAmount, List<int> PackagesAmount)
        {
            var prognosisDays = _context.Prognosis_Has_Days.Where(p => p.PrognosisId == prognosisId).ToList();
            if (prognosisDays.Count == CustomerAmount.Count && prognosisDays.Count == PackagesAmount.Count)
            {
                for (int i = 0; i < prognosisDays.Count; i++)
                {
                    prognosisDays[i].CustomerAmount = CustomerAmount[i];
                    prognosisDays[i].PackagesAmount = PackagesAmount[i];
                }

                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("CustomerAmount and PackagesAmount lists must have the same number of elements as prognosis days.");
            }
        }

        public Prognosis GetPrognosisById(int id)
        {
            return _context.Prognoses
                .Include(p => p.Prognosis_Has_Days)
                .FirstOrDefault(p => p.PrognosisId == id);
        }

        public int GetShelfMetersByPrognosis(int id)
        {
            return _context.Prognoses
                .Where(prognosis => prognosis.PrognosisId == id)
                .Join(
                    _context.Branches,
                    prognosis => prognosis.BranchId,
                    branch => branch.BranchId,
                    (prognosis, branch) => branch.shelfMeters
                )
                .FirstOrDefault();
        }

        public void DeletePrognosisById(int id)
        {
            var prognosis = _context.Prognoses.FirstOrDefault(p => p.PrognosisId == id);
            if (prognosis != null)
            {
                _context.Prognoses.Remove(prognosis);
                _context.SaveChanges();
            }
        }
        public List<PrognosisHasDaysHasDepartment> GetPrognosisDetailsByBranchWeekAndYear(int branchId, int weekNumber, int year)
        {
            return _context.PrognosisHasDaysHasDepartment
                .Include(phdd => phdd.PrognosisHasDays)
                    .ThenInclude(phd => phd.Prognosis)
                .Where(phdd => phdd.PrognosisHasDays.Prognosis.BranchId == branchId
                            && phdd.PrognosisHasDays.Prognosis.WeekNr == weekNumber
                            && phdd.PrognosisHasDays.Prognosis.Year == year)
                .ToList();
        }

    }
}