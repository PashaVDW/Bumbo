using bumbo.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Interfaces
{
    public interface ITemplatesRepository
    {
        List<Template> GetAllTemplates();

        Task<Template> GetByNameAndBranchAsync(string name, int branchId);

        Task<Template> GetByIdAsync(int templateId);

        Task Add(Template template);

        void Update(Template template);

        Task DeleteAsync(Template template);

        Task SaveChangesAsync();
    }
}
