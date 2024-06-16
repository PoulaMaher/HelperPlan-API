using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HelperPlan.Models;

namespace HelperPlan.DTO.dto.JobToModel
{
    public class JobToModelDTO
    {
        public int ID { get; set; }
        public string? Position { get; set; }
        public string? Type { get; set; }
        public string? Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployerID { get; set; }
        public int WorkingExperience { get; set; }

        public JTMCandidatePrefDTO? CandidatePref { get; set; }
        public List<JTMMainSkillsDTO>? MainSkills { get; set; }
        public List<JTMLanguagesDTO>? Languages { get; set; }
        public List<JTMCookingSkillsDTO>? CookingSkills { get; set; }
        public List<JTMOtherSkillsDTO>? OtherSkills { get; set; }
    }
}
