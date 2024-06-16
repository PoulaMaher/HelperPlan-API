using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.JobDTO
{
    public class JobDetails
    {
        public int Id { get; set; }
        public string? ImgUrl = "";
        public string JobTitle {  get; set; }
        public string JobDescription { get; set; }
        public bool ReceivePrivilegedAndDiscountOffers {  get; set; }
        public bool SubscribeToOurTipsAndNewsletters { get; set; }
    }
}
