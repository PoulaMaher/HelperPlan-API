using HelperPlan.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.JobDTO
{
    public class Job
    {
        public int Id { get; set; } 
        public JobRequirments? JobRequirments { get; set; }
        public AboutYou? AboutYou { get; set; }
        public JobDetails? JobDetails { get; set; }
        public Employer? Employer { get; set; }
        [ForeignKey("Employer")]
        public int EmployerId { get; set; }
        [ForeignKey("JobRequirments")]
        public int JobRequirmentsId { get; set; }
        [ForeignKey("AboutYou")]
        public int AboutYouId { get; set; }
        [ForeignKey("JobDetails")]
        public int JobDetailsId { get; set; }
    }
}
