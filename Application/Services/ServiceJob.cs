using Application.IServices;
using Domain;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services
{
    public class ServiceJob : IServiceJob
    {
        private readonly IRepositoryJob _repositoryJob;

        public ServiceJob(IRepositoryJob repositoryJob)
        {
            _repositoryJob = repositoryJob;
        }

        public async Task<DtoResponse<ApplyCompetitorJob>> ApplyNewJob(Guid idCompetitor, Guid idCreated, Guid idJob)
        {
            ApplyCompetitorJob applyCompetitorJob;
            Competitor competitor;

            Job job = (await _repositoryJob.GetJobById(idJob)).Data;
            ArgumentNullException.ThrowIfNull(job);

            competitor = (await _repositoryJob.GetApplyCompetitorJobById(idCompetitor)).Data;
            ArgumentNullException.ThrowIfNull(competitor);

            applyCompetitorJob =
                ApplyCompetitorJob.Create(idCompetitor, idJob, Guid.NewGuid(),
                idCreated);

            await _repositoryJob.SaveApplyCompetitorJob(applyCompetitorJob);
            return DtoResponse<ApplyCompetitorJob>.Create(applyCompetitorJob);

        }

        public async Task CreateJob(DtoRequest<Job> dtoRequest)
        {
            dtoRequest.Data.Id = Guid.NewGuid();
            dtoRequest.Data.IdUserCreated = Guid.NewGuid();
            dtoRequest.Data.UpdateDate = DateTime.Now;
            dtoRequest.Data.CreateDate = DateTime.Now;
            await _repositoryJob.SaveJob(dtoRequest.Data);
        }

        public async Task DeleteJob(DtoRequest<Guid> idJob)
        {
            await _repositoryJob.DeleteJob(idJob.Data);
        }

        public async Task<DtoResponse<Job>> GetDetailJob(Guid jobId)
        {
            DtoResponse<Job> dtoResponse = new DtoResponse<Job>();
            Job result = (await _repositoryJob.GetJobById(jobId)).Data;
            dtoResponse.Data = result;
            ArgumentNullException.ThrowIfNull(dtoResponse.Data);
            return dtoResponse;

        }

        public async Task<DtoResponse<List<Job>>> GetJobsList(int page, int pageSize, string search, string city)
        {
            PaginationList<List<Job>> jobs = await _repositoryJob.ListJobsByPage
                (page, pageSize, search);
            return DtoResponse<List<Job>>.Create(jobs.Data, jobs.Count);
        }

        public async Task UpdateJob(DtoRequest<Job> dtoRequest)
        {
            
        }

    }
}
