using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.JobDTO.JobSubBranches.AboutYouClass
{
    public class EmployerType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string? FamilyType { get; set; }
        public bool? HavePets { get; set; }
        public string? Nationality { get; set; }
    }
}
