using Application.IServices;
using Domain;
using Domain.Entities;
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

        public DtoResponse<List<Job>> GetJobsList(int take, int skip, string search)
        {
            List<Job> jobs = 
                _jobContext.Jobs.Where(job => job.NameJob.Contains(search))
                                .Skip(skip)
                                .Take(take)
                                .ToList();
            int count = _jobContext.Jobs.Where(job => job.NameJob.Contains(search)).Count();




            return new DtoResponse<List<Job>>() { Data = jobs, Count = count };
        }

    }
}
