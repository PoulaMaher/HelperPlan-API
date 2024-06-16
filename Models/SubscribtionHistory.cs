using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.Models
{
    public class SubscribtionHistory
    {
        public int ID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [ForeignKey("Employer")]
        public int UserID { get; set; }

        //Navigation Properties
        public ApplicationUser? Employer { get; set; }
    }
}
