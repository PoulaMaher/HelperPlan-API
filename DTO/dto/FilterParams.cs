namespace HelperPlan.DTO.dto
{
    public class FilterParams
    {
        public string[]? contractSelectedItems { get; set; }
        public string[]? languageSelectedItems { get; set; }
        public string[]? skillsSelectedItems { get; set; }
        public string[]? countrysSelectedItems { get; set; }
        public DateTime startdate { get; set; }
        public string? gender { get; set; }
        public string? JobPosition { get; set; }
        public string? JobType { get; set; }
        public int WorkingExperience { get; set; }
        public int Age { get; set; }
    }
}
