namespace HelperPlan.DTO.dto
{
    public class filtercandidateDTO
    {
        public string? Position { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Workexperience { get; set; }
        public int? Age { get; set; }
        public string? Jobtype { get; set; }
        public List<string>? Contract { get; set; }
        public List<string>? Language { get; set; }
        public List<string>? Mainskills { get; set; }
        public string? Gender { get; set; }
        public int pageIndex { get; set; }
        public int pageSize{ get; set; }
        public int pageCount { get; set; }

    }
}
