
using System;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IRepositoryJob
    {
        PaginationList<List<Job>> ListJobsByPage(int skip, int take, string search);
        
    }
}
