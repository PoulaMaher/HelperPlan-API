using System.ComponentModel.DataAnnotations;

namespace HelperPlan.DTO.SubscribtionDtos
{
    public class SubscribtionDto
    {
        public int ID { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive => false;
        [Required(ErrorMessage = "EmployerID is required")]
        public int EmployerID { get; set; }
        [Required(ErrorMessage = "PlanID is required")]
        public int PlanID { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

    }
}
