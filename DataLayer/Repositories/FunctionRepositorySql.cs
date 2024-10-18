using bumbo.Data;
using bumbo.Models;
using DataLayer.Interfaces;
using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    public class FunctionRepositorySql : IFunctionRepository
    {
        private readonly BumboDBContext _context;

        public FunctionRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public List<Function> GetAllFunctions()
        {
            return _context.Functions.ToList();
        }
    }
}
