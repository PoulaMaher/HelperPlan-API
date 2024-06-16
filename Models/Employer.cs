using HelperPlan.DTO.JobDTO;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelperPlan.Models
{
    public class Employer : ApplicationUser
    {
     
       
        public string? DayOff { get; set; }
        public string? Accomodation { get; set; }
        public decimal Salary { get; set; }
        public int KidsNo { get; set; }




        public int AdultNo { get; set; }
        public bool HasBet { get; set; }
        [Column(TypeName = "varchar(200)")]
       
        public string? title { get; set; }

        //Navigation Properties
        public Subscribtion? Subscribtion { get; set; }
        public ICollection<Job>? Jobs { get; set; }
        public ICollection<SubscribtionHistory>? SubscribtionHistories { get; set; }

    }
}
