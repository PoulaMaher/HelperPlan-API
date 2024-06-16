using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.dto.JobToModel
{
    public class JTMCandidatePrefDTO
    {
        public int ID { get; set; }
        public string? ContractStatus { get; set; }
        public string? Gender { get; set; }
        public string? Religion { get; set; }
        public string? EducationLevel { get; set; }
        public int Age { get; set; }
        public int JobID { get; set; }
    }
}
