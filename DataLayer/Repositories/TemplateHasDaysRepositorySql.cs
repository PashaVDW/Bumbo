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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
