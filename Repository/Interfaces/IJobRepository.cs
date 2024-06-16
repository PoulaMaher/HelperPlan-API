using HelperPlan.DTO.dto;
using HelperPlan.DTO.dto.JobToModel;
using HelperPlan.DTO.JobDTO;
using HelperPlan.Models;

namespace HelperPlan.Repository.Interfaces
{
    public interface IJobRepository : IRepository<Job>
    {
        void AddJob(Job job);
        List<Job> GetJobHeadlines();
        List<Job> GetFilteredJobs(FilterParams FilterParams);
        Job IncludeDetails(Job Job);
    }
}
