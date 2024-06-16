using HelperPlan.DTO.JobDTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.Models
{
    public class CandidatePref
    {
        public int ID { get; set; }
        public string? ContractStatus { get; set; }
        public string? Gender { get; set; }
        public string? Religion { get; set; }
        public string? EducationLevel { get; set; }
        public int Age { get; set; }
        [ForeignKey("Job")]
        public int JobID { get; set; }

        //Navigation Properties
        public Job? Job { get; set; }
    }
}
