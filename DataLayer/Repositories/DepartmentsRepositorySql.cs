using bumbo.Data;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class DepartmentsRepositorySql : IDepartmentsRepository
    {
        private readonly BumboDBContext _context;

        public DepartmentsRepositorySql(BumboDBContext context)
        {
            _context = context;
        }

        public bool IsValidDepartmentName(string departmentName)
        {
            string department = _context.Departments.SingleOrDefault(d => d.DepartmentName == departmentName).ToString();
            return department != null;
        }
    }
}
