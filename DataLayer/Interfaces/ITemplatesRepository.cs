using bumbo.Models;

namespace DataLayer.Interfaces
{
    public interface ITemplatesRepository
    {
        List<Template> GetAllTemplates();

        Task<Template> GetByNameAsync(string name);

        void Update(Template template);

        Task SaveChangesAsync();
    }
}
