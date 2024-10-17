using bumbo.Models;

namespace DataLayer.Interfaces
{
    public interface IDaysRepository
    {
        Task<Days> GetByNameAsync(string name);
    }
}
