using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.JobDTO
{
    public class AboutYou
    {
        public int Id { get; set; }
        [ForeignKey("AboutYouSub")]
        public int AboutYouSubId { get; set; }
        [ForeignKey("OffersToCandidate")]
        public int OffersToCandidateId { get; set; }
        public JobSubBranches.AboutYouClass.AboutYouSub? AboutYouSub { get; set; }
        public JobSubBranches.AboutYouClass.offersToCandidate? OffersToCandidate { get; set;}
    }
}
