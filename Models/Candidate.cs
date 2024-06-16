using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HelperPlan.Models
{
    public class Candidate : ApplicationUser
    {
  
        public string? Position { get; set; }
        public string? ContactEmail { get; set; }
        public string? PhotoURL { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public int NoKids { get; set; }
        public int workexperience { get; set; }

        public string? MartialStatus { get; set; }
        public string? Nationality { get; set; }
        public string? Religion { get; set; }
        public string? EducationLevel { get; set; }
  
      
        [Phone]
        public string? WhatappNumber { get; set; }
        public bool HasPassport { get; set; }
        public string? JobType { get; set; }
        public string? WorkStatus { get; set; }
        public DateTime? AvailabilityDate { get; set; }
        public decimal ExepectedSalary { get; set; }
        public string? PerferedDay { get; set; }
        public string? AccomodationPref { get; set; }


        //Navigatin Properties
        public ICollection<Experience>? Experiences { get; set; } 
        public ICollection<Education>? Educations { get; set; }
        public ICollection<MainSkills>? MainSkills { get; set; }
        public ICollection<Languages>? Languages { get; set; }
        public ICollection<OtherSkills>? OtherSkills { get; set; }
        public ICollection<CookingSkills>? CookingSkills { get; set; }
    }
}
