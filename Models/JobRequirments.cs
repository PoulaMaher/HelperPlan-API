using HelperPlan.DTO.JobDTO.JobSubBranches.JobRequirmentsClass;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.JobDTO
{
    public class JobRequirments
    {
        public int Id { get; set; }
        [ForeignKey("BasicInformation")]
        public int BasicInformationId { get; set; }
        [ForeignKey("RequiredSkillsAndDuties")]
        public int RequiredSkillsAndDutiesId { get; set; }
        [ForeignKey("CandidatePreference")]
        public int CandidatePreferenceId { get; set; }
        public BasicInformation? BasicInformation { get; set; }
        public RequiredSkillsAndDuties? RequiredSkillsAndDuties { get; set; }
        public CandidatePreference? CandidatePreference { get; set; }
    }
}
