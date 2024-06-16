using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.Models
{
    public class Education
    {
        [Key]
        public int ID { get; set; } 
        public string? EducationLevel { get; set; }
        public string? CrsDuration { get; set; }
        public bool HasComplete { get; set; }
        public DateTime? CompletionYear { get; set; }
        [ForeignKey("Candidate")]
        public int CandidateID { get; set; }

        //Navigation Properties
        public Candidate? Candidate { get; set; }
    }
}
