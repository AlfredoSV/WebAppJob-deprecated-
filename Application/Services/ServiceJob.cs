using Application.IServices;
using Domain;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Data;

namespace Application.Services
{
    public class ServiceJob : IServiceJob
    {
        private readonly JobContext _jobContext;
        private readonly IRepositoryJob _repositoryJob;

        public ServiceJob(JobContext jobContext, IRepositoryJob repositoryJob)
        {
            _jobContext = jobContext;
            _repositoryJob = repositoryJob;
        }

        public async Task<DtoResponse<ApplyCompetitorJob>> ApplyNewJob(Guid idCompetitor, Guid idCreated, Guid idJob)
        {
            ApplyCompetitorJob applyCompetitorJob = null;
            Job job = await _jobContext.Jobs.Where(jb => jb.Id == idJob).FirstOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(job);

            Competitor com = await _jobContext.Competitors.Where(com => com.Id == idCompetitor)
                                                    .FirstOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(com);

            using (IDbContextTransaction transaction = _jobContext.Database.
                BeginTransaction())
            {
                try
                {
                    job.VacancyNumbers = job.VacancyNumbers - 1;
                    _jobContext.Jobs.Update(job);
                    await _jobContext.SaveChangesAsync();

                    if (job.VacancyNumbers <= 0)
                        throw new ArgumentException("Vacancy Numbers is 0");

                    applyCompetitorJob =
                        ApplyCompetitorJob.Create(idCompetitor, idJob, Guid.NewGuid(),
                        idCreated);

                    _jobContext.CompetitorJobs.Add(applyCompetitorJob);
                    await _jobContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }



            }

            return new DtoResponse<ApplyCompetitorJob>()
            { Data = applyCompetitorJob };

        }

        public async Task CreateJob(DtoRequest<Job> dtoRequest)
        {
            dtoRequest.Data.Id = Guid.NewGuid();
            dtoRequest.Data.IdUserCreated = Guid.NewGuid();
            dtoRequest.Data.UpdateDate = DateTime.Now;
            dtoRequest.Data.CreateDate = DateTime.Now;
            _jobContext.Jobs.Add(dtoRequest.Data);
            await _jobContext.SaveChangesAsync();
        }

        public async Task DeleteJob(DtoRequest<Guid> idJob)
        {
            _jobContext.Jobs.Remove(_jobContext.Jobs.Where(jb => jb.Id == idJob.Data).First());
            await _jobContext.SaveChangesAsync();
        }

        public async Task<DtoResponse<Job>> GetDetailJob(Guid jobId)
        {
            DtoResponse<Job> dtoResponse = new DtoResponse<Job>();

            IQueryable<Job> result = _jobContext.Jobs.Where(job => job.Id == jobId)
                                                     .AsQueryable();

            dtoResponse.Data = await result.FirstOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(dtoResponse.Data);

            return dtoResponse;

        }

        public async Task<DtoResponse<List<Job>>> GetJobsList(int page, int pageSize, string search, string city)
        {
            PaginationList<List<Job>> jobs = await _repositoryJob.ListJobsByPage(page, pageSize, search);

            return new DtoResponse<List<Job>>() { Data = jobs.Data, Count = jobs.Count };
        }

        public async Task UpdateJob(DtoRequest<Job> dtoRequest)
        {
            Job job = (await GetDetailJob(dtoRequest.Data.Id)).Data;

            dtoRequest.Data.UpdateDate = DateTime.Now;
            dtoRequest.Data.CreateDate = job.CreateDate;
            dtoRequest.Data.IdUserCreated = job.IdUserCreated;

            _jobContext.Jobs.Update(dtoRequest.Data);
            _jobContext.SaveChanges();
        }

    }
}
