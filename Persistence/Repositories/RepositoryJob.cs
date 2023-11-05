

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
        private readonly JobContext _jobContext;

        public RepositoryJob(JobContext jobContext)
        {

            _jobContext = jobContext;
        }

        public async Task<PaginationList<List<Job>>> ListJobsByPage(int page, int pageSize, string search)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@page",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = page                        },
                        new SqlParameter() {
                            ParameterName = "@search",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = search
                        }, new SqlParameter() {
                            ParameterName = "@pageSize",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = pageSize
                        },
                        new SqlParameter() {
                            ParameterName = "@count",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Output,

                        }};

                List<Job> jobs = await _jobContext
                        .Jobs.FromSqlRaw("exec dbo.GetJobs @page, @pageSize, @search, @count out", param).ToListAsync();

                PaginationList<List<Job>> response = new PaginationList<List<Job>>();

                response.Data = jobs;
                response.Count = Convert.ToInt32(param[3].Value.ToString());

                return response;
            }
            catch (Exception e)
            {

                throw;
            }
            
           
        }
    }

}