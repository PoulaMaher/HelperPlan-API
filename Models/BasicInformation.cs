using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.JobDTO.JobSubBranches.JobRequirmentsClass
{
    public class BasicInformation
    {
        public int Id { get; set; }
        public string PositionOffered { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
    }
}
