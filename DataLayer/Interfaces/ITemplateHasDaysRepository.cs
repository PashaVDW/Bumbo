using bumbo.Models;

namespace DataLayer.Interfaces
{
    public interface ITemplateHasDaysRepository
    {
        List<TemplateHasDays> GetAllTemplateHasDays();

        void Update(TemplateHasDays templateHasDays);

        void DeleteByTemplateId(int templateId);

        Task SaveChangesAsync();
    }
}
