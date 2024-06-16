using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.Models
{
    public class Experience
    {
        [Key]
        public int ID { get; set; }
        public string? JobPosition { get; set; }
        public string? WorkingCountry { get; set; }
        public DateTime? StartYear { get; set; }
        public DateTime? EndYear { get; set; }
        public string? EmployerType { get; set; }
        public string? Duties { get; set; }
        public bool HasLetterRef { get; set; }
        [ForeignKey("Candidate")]
        public int CandidateID { get; set; }

        //Navigation Properties
        public Candidate? Candidate { get; set; }
    }
}
