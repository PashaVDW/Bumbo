using bumbo.Models;

namespace DataLayer.Interfaces
{
    public interface ITemplatesRepository
    {
        List<Template> GetAllTemplates();

        void Update(Template template);

        Task SaveChangesAsync();
    }
}
