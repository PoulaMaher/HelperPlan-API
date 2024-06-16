using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.Models
{
    public class Languages
    {
        [Key]
        public int ID { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        [ForeignKey("Candidate")]
        public int CandidateID { get; set; }

        //Navigation Properties
        public Candidate? Candidate { get; set; }
    }
}
