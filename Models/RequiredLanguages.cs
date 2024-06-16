using HelperPlan.DTO.JobDTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.Models
{
    public class RequiredLanguages
    {
        [Key]
        public int ID { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        [ForeignKey("Job")]
        public int JobID { get; set; }
        //Navigation Properties
        public Job? Job { get; set; }
    }
}
