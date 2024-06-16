using HelperPlan.Models;

namespace HelperPlan.DTO.dto
{
    public class candsDTO
    {
        public int? Id { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public string ContactEmail { get; set; }
        public string PhotoURL { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int NoKids { get; set; }
        public int WorkExperience { get; set; }
        public string MartialStatus { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string EducationLevel { get; set; }
        public string WhatappNumber { get; set; }
        public bool HasPassport { get; set; }
        public string JobType { get; set; }
        public string WorkStatus { get; set; }
        public DateTime AvailabilityDate { get; set; }
        public decimal ExpectedSalary { get; set; }
        public string PreferredDay { get; set; }
        public string AccommodationPref { get; set; }
        public List<ExperienceDTO> Experiences { get; set; }
        public List<EducationDTO> Educations { get; set; }
        public List<MainSkillsDTO> MainSkills { get; set; }
        public List<LanguagesDTO> Languages { get; set; } 
        public List<OtherSkillsDTO> OtherSkills { get; set; }
        public List<CookingSkillsDTO> CookingSkills { get; set; } 
    }
}
