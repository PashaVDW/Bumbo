﻿using bumbo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IDaysRepositorySQL
    {
        public List<Days> getAllDaysOrdered();
        List<Days> getAllDaysUnordered();
    }
}
