namespace HelperPlan.DTO.dto
{
    public class CandidatePageDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? Position { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Workexperience { get; set; }
        public int? Age { get; set; }
        public string? Jobtype { get; set; }
        public string? Contract { get; set; }
        public List<string>? Language { get; set; } = new List<string>();
        public List<string>? Mainskills { get; set; } = new List<string>();
        public string? Gender { get; set; }
        public string? Location { get; set; }
        public string? PhotoURL { get; set; }
    }
}
