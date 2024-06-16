using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.JobDTO
{
    public class RequiredSkillsAndDuties
    {
        public int Id { get; set; }
        public string[] LanguageSkills {  get; set; }
        public string[] MainSkills { get; set; }
        public string[] CookingSkills { get; set; }
        public string[] OtherSkills { get; set; }
        public string[] MostImportantSkills { get; set; }
    }
}