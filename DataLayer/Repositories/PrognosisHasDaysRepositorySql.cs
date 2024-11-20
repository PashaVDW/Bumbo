﻿using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class PrognosisHasDaysRepositorySql : IPrognosisHasDaysRepository
    {
        readonly BumboDBContext _context;

        public PrognosisHasDaysRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<PrognosisHasDays> GetPrognosis_has_days()
        {
            return _context.PrognosisHasDays.ToList();
        }
    }
}
