using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class TemplateHasDaysRepositorySql : ITemplateHasDaysRepository
    {
        readonly BumboDBContext _context;

        public TemplateHasDaysRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<TemplateHasDays> GetAllTemplateHasDays()
        {
            return _context.TemplateHasDays.ToList();
        }

        public void Update(TemplateHasDays templateHasDays)
        {
            _context.TemplateHasDays.Update(templateHasDays);
        }

        public void DeleteByTemplateId(int templateId)
        {
            var entries = _context.TemplateHasDays
                .Where(thd => thd.Templates_id == templateId)
                .ToList();

            _context.TemplateHasDays.RemoveRange(entries);
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
