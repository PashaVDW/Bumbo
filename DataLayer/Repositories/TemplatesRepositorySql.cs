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

        public Template GetByIdAndBranch(int templateId, int branchId)
        {
            return _context.Templates
                .FirstOrDefault(t => t.Id == templateId && t.Branch_branchId == branchId);
        }

        public async Task<Template> GetByNameAndBranchAndIdAsync(string name, int branchId, int templateId)
        {
            return await _context.Templates
                    .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower()
                    && t.Branch_branchId == branchId
                    && t.Id != templateId);
        }

        public async Task<Template> GetByIdAsync(int templateId)
        {
            return await _context.Templates
                .Include(t => t.TemplateHasDays)
                .FirstOrDefaultAsync(t => t.Id == templateId);
        }

        public async Task Add(Template template)
        {
            await _context.Templates.AddAsync(template);
        }

        public void Update(Template template)
        {
            _context.Templates.Update(template);
        }

        public async Task DeleteAsync(Template template)
        {
            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
