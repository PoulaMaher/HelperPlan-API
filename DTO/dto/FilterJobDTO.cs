namespace HelperPlan.DTO.dto
{
    public class FilterJobDTO
    {
        public List<string>? ContractSelectedItems { get; set; }
        public List<string>? LanguageSelectedItems { get; set; }
        public List<string>? SkillsSelectedItems { get; set; }
        public List<string>? CountrySelectedItems { get; set; }

        public DateTime? StartDate { get; set; }
        public string? Gender { get; set; }
        public string? JobPosition { get; set; }
        public string? JobType { get; set; }
        public int WorkingExperience {get; set;}
        public int Age { get; set;}

        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }

    }
}
