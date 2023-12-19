

using System.Numerics;
using System.Reflection.Emit;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Domain.Repositories
{
    public class RepositoryJob : IRepositoryJob
    {
        private readonly IDbContextFactory<JobContext> _jobContext;

        public RepositoryJob(IDbContextFactory<JobContext> jobContext)
        {

            _jobContext = jobContext;
        }

        public async Task<PaginationList<List<Job>>> ListJobsByPage(int page, int pageSize, string search)
        {
            try
            {
                List<Job> jobs = new List<Job>();

                PaginationList<List<Job>> response = new PaginationList<List<Job>>();

                using (JobContext jobContext = _jobContext.CreateDbContext())
                {
                    if (page != 1)
                        page--;
                    else
                        page = 0;

                    if (string.IsNullOrEmpty(search))
                    {
                        response.Data = await jobContext
                            .Jobs.AsQueryable().Take(pageSize).Skip(page * pageSize).ToListAsync();
                        response.Count = jobContext
                            .Jobs.AsQueryable().Count();
                    }


                    if (!string.IsNullOrEmpty(search))
                    {
                        response.Data = await jobContext
                            .Jobs.AsQueryable().Where(st => st.DescriptionJob.Contains(search) && st.NameJob.Contains(search))
                            .Take(pageSize).Skip(page * pageSize).ToListAsync();
                        response.Count = jobContext
                            .Jobs.AsQueryable().Where(st => st.DescriptionJob.Contains(search) && st.NameJob.Contains(search))
                            .Count();

                    }
                }
               
                return response;
            }
            catch (Exception e)
            {

                throw;
            }


        }
    }

}