using AutoMapper;
using Domain.Entities;
using System.Net;
using WebAppJob.Models;

namespace WebAppJob.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Job, JobViewModel>();
            CreateMap<JobViewModel, Job>();

        }
    }
}
