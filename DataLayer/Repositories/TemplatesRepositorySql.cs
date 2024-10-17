using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class TemplatesRepositorySql : ITemplatesRepository
    {

        readonly BumboDBContext _context;

        public TemplatesRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<Template> GetAllTemplates()
        { 
            return _context.Templates.ToList();
        }

        public async Task<Template> GetByNameAsync(string name)
        {
            return await _context.Templates
                .FirstOrDefaultAsync(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void Update(Template template)
        {
            _context.Templates.Update(template);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
