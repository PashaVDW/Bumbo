﻿using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class PrognosisRepositorySql : IPrognosisRepository
    {
        readonly BumboDBContext _context;

        public PrognosisRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<Prognosis> GetAllPrognosis()
        {
            return _context.Prognoses.ToList();
        }

        public Prognosis GetLatestPrognosis()
        {
            List<Prognosis> prognosis = _context.Prognoses
                .Include(p => p.Prognosis_Has_Days)
                    .ThenInclude(phd => phd.prognosis_Has_Days_Has_Department)
                .OrderByDescending(p => p.Year)
                .ThenByDescending(p => p.WeekNr)
                .ToList<Prognosis>();
            return prognosis.First();
        }

        public Prognosis GetPrognosisByWeekAndYear(int weekNumber, int year)
        {
            List<Prognosis> prognosis = _context.Prognoses
                .Include(p => p.Prognosis_Has_Days)
                    .ThenInclude(phd => phd.prognosis_Has_Days_Has_Department)
                .Where(p => p.WeekNr == weekNumber && p.Year == year)
                .ToList();

            return prognosis.FirstOrDefault();
        }

    }
}
