
﻿using bumbo.Models;
using DataLayer.Models.DTOs;

namespace DataLayer.Interfaces
{
    public interface INormsRepository
    {
        Task<List<Norm>> GetNorms();
        Task<List<NormOverviewDTO>> GetOverview();
        Task InsertMany(Norm[] norms);
        Task UpdateMany(List<Norm> norms);
        Task<Norm> GetNorm(int selectedNormId);
        Task<List<Norm>> GetSelectedNorms(int? branchId, int year, int week);
        Task<List<Norm>> GetLatestNorm();
    }
}
