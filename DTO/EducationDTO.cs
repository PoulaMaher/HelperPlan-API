namespace HelperPlan.DTO.dto
{
    public class EducationDTO
    {
        public int? Id { get; set; }
        public string? EducationLevel { get; set; }
        public string? CrsDuration { get; set; }
        public bool HasComplete { get; set; }
        public DateTime? CompletionYear { get; set; }
        public int CandidateID { get; set; }

    }
}
