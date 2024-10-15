using bumbo.Data;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class NormsRepositorySql : INormsRepository
    {
        readonly BumboDBContext _context;
        
    }
}
