using Domain;
using Domain.Entities;

namespace Application.IServices
{
    public interface IServiceJob
    {
        Task<DtoResponse<List<Job>>> GetJobsList(int page, int pageSize, string search, string city);
        Task CreateJob(DtoRequest<Job> dtoRequest);
        Task<DtoResponse<Job>> GetDetailJob(Guid jobId);
        Task<DtoResponse<ApplyCompetitorJob>> ApplyNewJob(Guid idCompetitor,
            Guid idCreated, Guid idJob);
        Task DeleteJob(DtoRequest<Guid> idJob);
        Task UpdateJob(DtoRequest<Job> dtoRequest);


	}
}
