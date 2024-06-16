using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.JobDTO.JobSubBranches.AboutYouClass
{
    public class AboutYouSub
    {
        public int Id { get; set; }
        [ForeignKey("EmployerType")]
        public int EmployerTypeId { get; set; }
        public bool ReceiveByEmail { get; set; }
        public string Email { get; set; }
        public EmployerType? EmployerType { get; set; }
    }
}
