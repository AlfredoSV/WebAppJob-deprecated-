using Domain.Entities;
using Domain.IRepositories;
using Framework.Utilities2023.IServices;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Domain.Repositories
{
    public class RepositoryJob : IRepositoryJob
    {
        private readonly IDbContextFactory<JobContext> _jobContext;

        public IServiceLogBook ServiceLogBook { get; set; }

        public RepositoryJob(IDbContextFactory<JobContext> jobContext, IServiceLogBook serviceLogBook)
        {
            _jobContext = jobContext;
            this.ServiceLogBook = serviceLogBook;
        }


        public async Task<PaginationList<List<Job>>> ListJobsByPage(int page, int pageSize, string search)
        {
            PaginationList<List<Job>> response = new PaginationList<List<Job>>();
            
            try
            {
                List<Job> jobs = new List<Job>();              

                using (JobContext jobContext = _jobContext.CreateDbContext())
                {
                    page = (page != 1) ? page-- : 0;

                    if (string.IsNullOrEmpty(search))
                    {
                        response.Data = await jobContext
                            .Jobs.AsQueryable().Take(pageSize).Skip(page * pageSize).ToListAsync();
                        response.Count = jobContext
                            .Jobs.AsQueryable().Count();
                    }else 
                    {
                        response.Data = await jobContext
                            .Jobs.AsQueryable().Where(st => st.DescriptionJob.Contains(search) || 
                            st.NameJob.Contains(search))
                            .Take(pageSize).Skip(page * pageSize).ToListAsync();
                        response.Count = jobContext
                            .Jobs.AsQueryable().Where(st => st.DescriptionJob.Contains(search) || st.NameJob.Contains(search))
                            .Count();

                    }
                }
               
            }
            catch (Exception e)
            {
                await this.ServiceLogBook.SaveErrorLog(e);
            }

            return response;
        }

        public async Task<DtoResponse<Job>> GetJobById(Guid id)
        {
            try
            {

                using (JobContext jobContext = _jobContext.CreateDbContext())
                {
                    return DtoResponse<Job>.Create(await
                        jobContext.Jobs.AsQueryable().FirstOrDefaultAsync(j => j.Id.Equals(id)));
                }


            }
            catch (Exception e)
            {
                await this.ServiceLogBook.SaveErrorLog(e);
            }
            return DtoResponse<Job>.Create(new Job());
        }

        public async Task UpdateVacancyNumbers(Guid id)
        {

            try
            {

                using (JobContext jobContext = _jobContext.CreateDbContext())
                {
                    Job job = (await GetJobById(id)).Data;
                    if (job.VacancyNumbers <= 0)
                        throw new ArgumentException("Vacancy Numbers is 0");

                    job.VacancyNumbers = job.VacancyNumbers - 1;
                    jobContext.Jobs.Update(job);
                    await jobContext.SaveChangesAsync();         

                }

            }
            catch (Exception e)
            {
                await this.ServiceLogBook.SaveErrorLog(e);
            }

        }

        public async Task<DtoResponse<Competitor>> GetApplyCompetitorJobById(Guid id)
        {
            try
            {

                using (JobContext jobContext = _jobContext.CreateDbContext())
                {
                    return DtoResponse<Competitor>.Create(await
                        jobContext.Competitors.AsQueryable().FirstOrDefaultAsync(j => j.Id.Equals(id)));
                }


            }
            catch (Exception e)
            {
                await this.ServiceLogBook.SaveErrorLog(e);
            }

            return DtoResponse<Competitor>.Create(new Competitor());

        }

        public async Task SaveApplyCompetitorJob(ApplyCompetitorJob applyCompetitorJob)
        {

            try
            {

                using (JobContext jobContext = _jobContext.CreateDbContext())
                {
                    await jobContext.AddAsync(applyCompetitorJob);
                    await jobContext.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                await this.ServiceLogBook.SaveErrorLog(e);
            }
        }

        public async Task<DtoResponse> DeleteJob(Guid id)
        {
            try
            {

                using (JobContext jobContext = _jobContext.CreateDbContext())
                {
                    jobContext.Remove(new Job() { Id = id});
                    await jobContext.SaveChangesAsync();
                    return DtoResponse.Create(StatusRequest.Ok);
                }


            }
            catch (Exception e)
            {
                await this.ServiceLogBook.SaveErrorLog(e);
                return DtoResponse.Create(StatusRequest.Error);
            }

        }

        public async Task<DtoResponse> SaveJob(Job job)
        {
            try
            {

                using (JobContext jobContext = _jobContext.CreateDbContext())
                {
                    await jobContext.AddAsync(job);
                    await jobContext.SaveChangesAsync();
                    return DtoResponse.Create(StatusRequest.Ok);
                }


            }
            catch (Exception e)
            {
                await this.ServiceLogBook.SaveErrorLog(e);
                return DtoResponse.Create(StatusRequest.Error);
            }

        }

        public async Task<DtoResponse> UpdateJob(Job job)
        {
            try
            {

                using (JobContext jobContext = _jobContext.CreateDbContext())
                {
                    Job jobRes = (await GetJobById(job.Id)).Data;

                    job.UpdateDate = DateTime.Now;
                    job.CreateDate = job.CreateDate;
                    job.IdUserCreated = job.IdUserCreated;
                    jobContext.Jobs.Update(job);
                    await jobContext.SaveChangesAsync();
                }


            }
            catch (Exception e)
            {
                await this.ServiceLogBook.SaveErrorLog(e);
                return DtoResponse.Create(StatusRequest.Error);
            }

            return DtoResponse.Create(StatusRequest.OperationNotPerformed);
        }
    }

}