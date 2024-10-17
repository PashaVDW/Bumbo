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

        public async Task<Template> GetByNameAndBranchAsync(string name, int branchId)
        {
            return await _context.Templates
                .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower()
                    && t.Branch_branchId == branchId);
        }

        public async Task Add(Template template)
        {
            await _context.Templates.AddAsync(template);
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
