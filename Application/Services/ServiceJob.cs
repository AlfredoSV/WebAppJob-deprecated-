using Application.IServices;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Data;

namespace Application.Services
{
    public class ServiceJob : IServiceJob
    {
        private readonly JobContext _jobContext;

        public ServiceJob(JobContext jobContext)
        {
            _jobContext = jobContext;
        }

        public DtoResponse<ApplyCompetitorJob> ApplyNewJob(Guid idCompetitor,Guid idCreated,Guid idJob)
        {
            ApplyCompetitorJob? applyCompetitorJob = null;
            Job? job = _jobContext.Jobs.Where(jb => jb.Id == idJob).FirstOrDefault();

            ArgumentNullException.ThrowIfNull(job);

            Competitor? com = _jobContext.Competitors.Where(com => com.Id == idCompetitor)
                                                    .FirstOrDefault();

            ArgumentNullException.ThrowIfNull(com);
            
            using IDbContextTransaction transaction = _jobContext.Database.
                BeginTransaction();
            {
                try
                {
                    job.VacancyNumbers = job.VacancyNumbers - 1;
                    _jobContext.Jobs.Update(job);
                    _jobContext.SaveChanges();

                    if (job.VacancyNumbers <= 0)
                        throw new ArgumentException("Vacancy Numbers is 0");

                    applyCompetitorJob =
                        ApplyCompetitorJob.Create(idCompetitor, idJob, Guid.NewGuid(),
                        idCreated);

                    _jobContext.CompetitorJobs.Add(applyCompetitorJob);
                    _jobContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                
            }

            return new DtoResponse<ApplyCompetitorJob>()
            { Data = applyCompetitorJob };

        }

        public void CreateJob(DtoRequest<Job> dtoRequest)
        {
			dtoRequest.Data.Id = Guid.NewGuid();
			dtoRequest.Data.IdUserCreated = Guid.NewGuid();
			dtoRequest.Data.UpdateDate = DateTime.Now;
			dtoRequest.Data.CreateDate = DateTime.Now;
			_jobContext.Jobs.Add(dtoRequest.Data);
            _jobContext.SaveChanges();
        }

        public void DeleteJob(DtoRequest<Guid> idJob)
        {
           _jobContext.Jobs.Remove(_jobContext.Jobs.Where(jb => jb.Id == idJob.Data).First());
            _jobContext.SaveChanges();
        }

        public DtoResponse<Job> GetDetailJob(Guid jobId)
        {
            DtoResponse<Job> dtoResponse = new DtoResponse<Job>();

            IQueryable<Job> result = _jobContext.Jobs.Where(job => job.Id == jobId)
                                                     .AsQueryable();

            dtoResponse.Data =  result.FirstOrDefault();

            ArgumentNullException.ThrowIfNull(dtoResponse.Data);

            return dtoResponse;

        }

        public DtoResponse<List<Job>> GetJobsList(int take, int skip, string search)
        {
            List<Job> jobs = 
                _jobContext.Jobs.Where(job => job.NameJob.Contains(search))
                                .Skip(skip)
                                .Take(take)
                                .ToList();

            int count = _jobContext.Jobs.Where(job => job.NameJob.Contains(search))
                                        .Count();

            return new DtoResponse<List<Job>>() { Data = jobs, Count = count };
        }
        
        public void UpdateJob(DtoRequest<Job> dtoRequest)
        {
	
			dtoRequest.Data.UpdateDate = DateTime.Now;
			_jobContext.Jobs.Update(dtoRequest.Data);
			_jobContext.SaveChanges();
		}
       
    }
}
