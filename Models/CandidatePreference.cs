using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace HelperPlan.DTO.JobDTO.JobSubBranches.JobRequirmentsClass
{
    public class CandidatePreference
    {
          public int Id { get; set; }
          public string PreferedCandidateLocation { get; set; }
          public string PreferedCandidateContract { get; set; }
          public string Gender { get; set; }
          public string[] Nationality { get; set; }
          public string Education { get; set; }
          public string[] Religion { get; set; }
          public int[] AgeRequired { get; set; }
          public int[] ExperienceYearsRequired { get; set; }
    }
}
