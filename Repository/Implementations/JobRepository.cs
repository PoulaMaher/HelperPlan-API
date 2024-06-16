using HelperPlan.DTO.dto.JobToModel;
using HelperPlan.DTO.dto;
using HelperPlan.DTO.JobDTO;
using HelperPlan.Models;
using HelperPlan.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using HelperPlan.DTO.JobDTO.JobSubBranches.JobRequirmentsClass;
using Microsoft.AspNetCore.Mvc;

namespace HelperPlan.Repository.Implementations
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly Context dbcontext;
        public JobRepository(Context _dbcontext) : base(_dbcontext)
        {
            dbcontext = _dbcontext;
        }
        public void AddJob(Job job)
        {
            dbcontext.BasicInformation.Add(job.JobRequirments.BasicInformation);
            dbcontext.RequiredSkillsAndDuties.Add(job.JobRequirments.RequiredSkillsAndDuties);
            dbcontext.CandidatePreference.Add(job.JobRequirments.CandidatePreference);
            dbcontext.SaveChanges();
            JobRequirments NewRequirments = new JobRequirments
            {
                BasicInformationId = job.JobRequirments.BasicInformation.Id,
                RequiredSkillsAndDutiesId = job.JobRequirments.RequiredSkillsAndDuties.Id,
                CandidatePreferenceId = job.JobRequirments.CandidatePreference.Id,
            };
            dbcontext.JobRequirments.Add(NewRequirments);
            dbcontext.SaveChanges();
            dbcontext.EmployerType.Add(job.AboutYou.AboutYouSub.EmployerType);
            dbcontext.MonthlySalaryOffer.Add(job.AboutYou.OffersToCandidate.MonthlySalaryOffer);
            dbcontext.SaveChanges();
            dbcontext.offersToCandidate.Add(job.AboutYou.OffersToCandidate);
            dbcontext.AboutYouSub.Add(job.AboutYou.AboutYouSub);
            dbcontext.SaveChanges();
            AboutYou NewaboutYou = new AboutYou
            {
                AboutYouSubId = job.AboutYou.AboutYouSub.Id,
                OffersToCandidateId = job.AboutYou.OffersToCandidate.Id,
            };
            dbcontext.AboutYou.Add(NewaboutYou);
            dbcontext.JobDetails.Add(job.JobDetails);
            dbcontext.SaveChanges();
            Job NewJob = new Job
            {
                JobRequirmentsId = NewRequirments.Id,
                AboutYouId = NewaboutYou.Id,
                JobDetailsId = job.JobDetails.Id,
                EmployerId = job.EmployerId
            };
            dbcontext.Jobs.Add(NewJob);
            dbcontext.SaveChanges();
        }
        public List<Job> GetJobHeadlines()
        {
            List<Job> jobs = this.dbcontext.Jobs.Include((j) => j.JobRequirments.BasicInformation).ToList();
            return jobs;
        }
        public List<Job> GetFilteredJobs([FromQuery]FilterParams FilterParams)
        {
            List<Job> jobs = this.dbcontext.Jobs.Include((j) =>
            j.JobRequirments.BasicInformation).Include((j) => j.JobRequirments.CandidatePreference).Include(j=>j.JobRequirments.RequiredSkillsAndDuties).ToList();
            jobs = jobs.Where((j) => IsInListOrNot<string>(FilterParams.countrysSelectedItems,j.JobRequirments.BasicInformation.Location)).ToList();
            jobs = jobs.Where((j) => IsInListOrNot<string>(FilterParams.contractSelectedItems, j.JobRequirments.CandidatePreference.PreferedCandidateContract)).ToList();
            jobs = jobs.Where((j) => IsIncludeOrNot(FilterParams.languageSelectedItems, j.JobRequirments.RequiredSkillsAndDuties.LanguageSkills)).ToList();
            jobs = jobs.Where((j) => IsIncludeOrNot(FilterParams.skillsSelectedItems, j.JobRequirments.RequiredSkillsAndDuties.MostImportantSkills)).ToList();
            jobs = jobs.Where((j) => FilterParams.Age >= j.JobRequirments.CandidatePreference.AgeRequired[0] && FilterParams.Age <= j.JobRequirments.CandidatePreference.AgeRequired[1]).ToList();
            jobs = jobs.Where((j) => FilterParams.WorkingExperience >= j.JobRequirments.CandidatePreference.ExperienceYearsRequired[0] && FilterParams.WorkingExperience <= j.JobRequirments.CandidatePreference.ExperienceYearsRequired[1]).ToList();
            jobs = jobs.Where((j) => WordMatchedOrNot(FilterParams.JobType, j.JobRequirments.BasicInformation.Type)).ToList();
            jobs = jobs.Where((j) => WordMatchedOrNot(FilterParams.JobPosition,j.JobRequirments.BasicInformation.PositionOffered)).ToList();
            jobs = jobs.Where((j) => WordMatchedOrNot(FilterParams.gender, j.JobRequirments.CandidatePreference.Gender)).ToList();
            jobs = jobs.Where((j) => FilterParams.startdate<j.JobRequirments.BasicInformation.StartDate).ToList();
            return jobs;
        }
        private bool IsIncludeOrNot(string[] querySelectedItems, string[] jobItems)
        {
            if (querySelectedItems[0] == null)
                return true;

            foreach (var item in querySelectedItems)
            {
                foreach(var item2 in jobItems)
                {
                    if (item.Equals(item2))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool IsInListOrNot<T>(T[] SearchedList,T RequiredToSearch)
        {
            if(SearchedList[0] == null)
            return true;

            return SearchedList.Contains(RequiredToSearch);
        }
        private bool WordMatchedOrNot(string Source, string Destination)
        {
            if (Source == "Any" )
                return true;


            return Source.Equals(Destination);
        }

        public Job IncludeDetails(Job Job)
        {
            Job job = dbcontext.Jobs.Where((j) => j.Id == Job.Id).Include(j => j.JobRequirments.BasicInformation).
                Include(j => j.JobRequirments.RequiredSkillsAndDuties).
                Include(j => j.JobRequirments.CandidatePreference).
                Include(j => j.AboutYou.AboutYouSub).
                Include(j => j.AboutYou.AboutYouSub.EmployerType).
                Include(j => j.AboutYou.OffersToCandidate.MonthlySalaryOffer).
                Include(j => j.AboutYou.OffersToCandidate).
                Include(j => j.JobDetails).FirstOrDefault();
            return job;
        }
    }
}
