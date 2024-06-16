using AutoMapper;
using HelperPlan.DTO.dto;
using HelperPlan.DTO.dto.JobToModel;
using HelperPlan.DTO.JobDTO;
using HelperPlan.DTO.SubscribtionDtos;
using HelperPlan.Models;

namespace HelperPlan
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<candsDTO, Candidate>()
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.ContactEmail))
            .ForMember(dest => dest.PhotoURL, opt => opt.MapFrom(src => src.PhotoURL))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.NoKids, opt => opt.MapFrom(src => src.NoKids))
            .ForMember(dest => dest.workexperience, opt => opt.MapFrom(src => src.WorkExperience))
            .ForMember(dest => dest.MartialStatus, opt => opt.MapFrom(src => src.MartialStatus))
            .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
            .ForMember(dest => dest.Religion, opt => opt.MapFrom(src => src.Religion))
            .ForMember(dest => dest.EducationLevel, opt => opt.MapFrom(src => src.EducationLevel))
            .ForMember(dest => dest.WhatappNumber, opt => opt.MapFrom(src => src.WhatappNumber))
            .ForMember(dest => dest.HasPassport, opt => opt.MapFrom(src => src.HasPassport))
            .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType))
            .ForMember(dest => dest.WorkStatus, opt => opt.MapFrom(src => src.WorkStatus))
            .ForMember(dest => dest.AvailabilityDate, opt => opt.MapFrom(src => src.AvailabilityDate))
            .ForMember(dest => dest.ExepectedSalary, opt => opt.MapFrom(src => src.ExpectedSalary))
            .ForMember(dest => dest.PerferedDay, opt => opt.MapFrom(src => src.PreferredDay))
            .ForMember(dest => dest.AccomodationPref, opt => opt.MapFrom(src => src.AccommodationPref));

            CreateMap<ExperienceDTO, Experience>();
            CreateMap<EducationDTO, Education>();
            CreateMap<LanguagesDTO, Languages>();
            CreateMap<MainSkillsDTO, MainSkills>();
            CreateMap<OtherSkillsDTO, OtherSkills>();
            CreateMap<CookingSkillsDTO, CookingSkills>();
            /////////////////
            ///
            CreateMap<Candidate, candsDTO>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
    .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.ContactEmail))
    .ForMember(dest => dest.PhotoURL, opt => opt.MapFrom(src => src.PhotoURL))
    .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
    .ForMember(dest => dest.NoKids, opt => opt.MapFrom(src => src.NoKids))
    .ForMember(dest => dest.WorkExperience, opt => opt.MapFrom(src => src.workexperience))
    .ForMember(dest => dest.MartialStatus, opt => opt.MapFrom(src => src.MartialStatus))
    .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
    .ForMember(dest => dest.Religion, opt => opt.MapFrom(src => src.Religion))
    .ForMember(dest => dest.EducationLevel, opt => opt.MapFrom(src => src.EducationLevel))
    .ForMember(dest => dest.WhatappNumber, opt => opt.MapFrom(src => src.WhatappNumber))
    .ForMember(dest => dest.HasPassport, opt => opt.MapFrom(src => src.HasPassport))
    .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType))
    .ForMember(dest => dest.WorkStatus, opt => opt.MapFrom(src => src.WorkStatus))
    .ForMember(dest => dest.AvailabilityDate, opt => opt.MapFrom(src => src.AvailabilityDate))
    .ForMember(dest => dest.ExpectedSalary, opt => opt.MapFrom(src => src.ExepectedSalary))
    .ForMember(dest => dest.PreferredDay, opt => opt.MapFrom(src => src.PerferedDay))
    .ForMember(dest => dest.AccommodationPref, opt => opt.MapFrom(src => src.AccomodationPref));

            CreateMap<Experience, ExperienceDTO>();
            CreateMap<Education, EducationDTO>();
            CreateMap<Languages, LanguagesDTO>();
            CreateMap<MainSkills, MainSkillsDTO>();
            CreateMap<OtherSkills, OtherSkillsDTO>();
            CreateMap<CookingSkills, CookingSkillsDTO>();


           CreateMap<JobToModelDTO, Job>();
            CreateMap<JTMCandidatePrefDTO, CandidatePref>();
            CreateMap<JTMCookingSkillsDTO, RequiredCookingSkills>();
            CreateMap<JTMLanguagesDTO, RequiredLanguages>();
            CreateMap<JTMMainSkillsDTO, RequiredMainSkills>();
            CreateMap<JTMOtherSkillsDTO, RequiredOtherSkills>();



            CreateMap<SubscribtionDto, Subscribtion>().ReverseMap();

        }
    }
}
