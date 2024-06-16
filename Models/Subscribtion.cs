using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.Models
{
    public class Subscribtion
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("Employer")]
        public int UserID { get; set; }
        [ForeignKey("Plan")]
        public int PlanID { get; set; }

        public int PlanID1 { get; set; }
        // Additional properties
        public string? SubscribtionsStatus { get; set; }
        public string? TransactionNo { get; set; }
        public string? CheckUrl { get; set; }
        public string? orderNumber { get; set; }
        public ApplicationUser? Employer { get; set; }
        public Plan? Plan { get; set; }
    }
}
