using Domain;
using Domain.Entities;

namespace Application.IServices
{
    public interface IServiceJob
    {
        DtoResponse<List<Job>> GetJobsList(int page, int pageSize, string search);
        void CreateJob(DtoRequest<Job> dtoRequest);
        DtoResponse<Job> GetDetailJob(Guid jobId);
        DtoResponse<ApplyCompetitorJob> ApplyNewJob(Guid idCompetitor,
            Guid idCreated, Guid idJob);
        void DeleteJob(DtoRequest<Guid> idJob);
        void UpdateJob(DtoRequest<Job> dtoRequest);


	}
}
