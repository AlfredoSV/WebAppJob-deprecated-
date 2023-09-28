

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

        public PaginationList<List<Job>> ListJobsByPage(int skip, int take, string search)
        {
           SqlParameter[] param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@skip",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = skip                        },
                        new SqlParameter() {
                            ParameterName = "@take",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = skip
                        },
                        new SqlParameter() {
                            ParameterName = "@count",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Output
                        }};

            List<Job> jobs = _jobContext
                    .Jobs.FromSqlRaw("exec dbo.GetJobs @skip, @take, @count", param).ToList();

            PaginationList<List<Job>> response = new PaginationList<List<Job>>();

            response.Data = jobs;
            response.Count = Int32.Parse(param[2].Value.ToString());

            return response;
        }
    }

}