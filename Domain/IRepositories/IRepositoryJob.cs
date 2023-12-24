
using System;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IRepositoryJob
    {
        Task<PaginationList<List<Job>>> ListJobsByPage(int page, int pageSize, string search);
        Task<DtoResponse<Job>> GetJobById(Guid id);
        Task<DtoResponse> DeleteJob(Guid id);
        Task<DtoResponse> SaveJob(Job job);
        Task<DtoResponse> UpdateJob(Job job);
        Task UpdateVacancyNumbers(Guid id);
        Task<DtoResponse<Competitor>> GetApplyCompetitorJobById(Guid id);
        Task SaveApplyCompetitorJob(ApplyCompetitorJob applyCompetitorJob);

    }
}
