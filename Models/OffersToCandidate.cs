using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.DTO.JobDTO.JobSubBranches.AboutYouClass
{
    public class offersToCandidate
    { 
       public int Id { get; set; }
       [ForeignKey("MonthlySalaryOffer")]
       public int MonthlySalaryOfferId { get; set; }
       public string DayOFF {  get; set; }
       public string Accomodation { get; set; }
       public AboutYouClass.OffersToCandidate.MonthlySalaryOffer? MonthlySalaryOffer { get; set; }
    }
}
