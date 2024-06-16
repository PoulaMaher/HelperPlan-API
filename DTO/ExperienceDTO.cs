namespace HelperPlan.DTO.dto
{
    public class ExperienceDTO
    {
        public int? Id { get; set; }
        public string? JobPosition { get; set; }
        public string? WorkingCountry { get; set; }
        public DateTime? StartYear { get; set; }
        public DateTime? EndYear { get; set; }
        public string? EmployerType { get; set; }
        public string? Duties { get; set; }
        public bool HasLetterRef { get; set; }
        public int CandidateID { get; set; }

    }
}
