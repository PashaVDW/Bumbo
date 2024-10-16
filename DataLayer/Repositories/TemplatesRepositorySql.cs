using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;

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

    }
}
