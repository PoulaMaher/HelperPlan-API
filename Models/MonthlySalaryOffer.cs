using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.JobDTO.JobSubBranches.AboutYouClass.OffersToCandidate
{
    public class MonthlySalaryOffer
    {
        public int Id {  get; set; }
        public string? Title { get; set; }
        public int? MonthlySalary { get; set; }
        public int? MaxSalary { get; set; }
        public int? MinSalary { get; set; }
        public string? Currency { get; set; }
        public string? Description { get; set; }
    }
}
