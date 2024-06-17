using HelperPlan.DTO.dto;
using HelperPlan.Models;
using HelperPlan.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace HelperPlan.Repository.Implementations
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        private readonly Context dbcontext;
        public CandidateRepository(Context _dbcontext) : base(_dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public IEnumerable<Candidate> filteredcandidate(filtercandidateDTO fcd, string? includeprop = null)
        {

            IQueryable<Candidate> query = GetList(c => c.description != "" || c.description != null && c.Position != "" || c.Position != null && c.PhotoURL != null || c.PhotoURL != "", includeprop).AsQueryable();

            //Apply filtering based on provided parameters
            if (!string.IsNullOrEmpty(fcd.Position))
            {
                query = query.Where(p => p.Position == fcd.Position);
            }
            if (fcd.Workexperience != 0)
            {
                query = query.Where(candidate => candidate.workexperience >= fcd.Workexperience);
            }

            if (!string.IsNullOrEmpty(fcd.Jobtype))
            {
                query = query.Where(p => p.JobType == fcd.Jobtype);
            }

            if (fcd.Age != 0)
            {
                query = query.Where(p => p.Age >= fcd.Age);
            }
            //if (fcd.Contract != null && fcd.Contract.Any())
            //{
            //    query = query.Where(candidate => fcd.Contract.Contains(candidate.WorkStatus));
            //}
            if (fcd.Language != null && fcd.Language.Any())
            {
                query = query.Where(candidate => candidate.Languages.Any(lang => fcd.Language.Contains(lang.Name)));
            }
            if (fcd.Mainskills != null && fcd.Mainskills.Any())
            {
                query = query.Where(candidate => candidate.MainSkills.Any(skill => fcd.Mainskills.Contains(skill.Name)));
            }
            if (!string.IsNullOrEmpty(fcd.Gender))
            {
                query = query.Where(candidate => candidate.Gender == fcd.Gender);
            }
            //if (fcd.StartDate.HasValue)
            //{
            //    query = query.Where(candidate => candidate.AvailabilityDate >= fcd.StartDate);
            //}

            query = query.Skip(fcd.pageIndex * fcd.pageSize).Take(fcd.pageSize);
            // Execute the query and return the result
            var candiates = query.ToList();

            return candiates;
        }

        public IEnumerable<CandidatePageDTO> mapandget(IEnumerable<Candidate> mycan)
        {
            ICollection<CandidatePageDTO> mycpd = new List<CandidatePageDTO>();

            foreach (Candidate can in mycan)
            {
                CandidatePageDTO cd = new CandidatePageDTO();
                cd.Id= can.Id;
                cd.Name = can.Fname + ' ' + can.Lname;
                cd.Description = can.description;
                cd.Position = can.Position;
                cd.Location = can.Location;
                cd.PhotoURL = can.PhotoURL;
                cd.StartDate = can.AvailabilityDate;
                cd.Workexperience = can.workexperience;
                cd.Age = can.Age;
                cd.Jobtype = can.JobType;
                cd.Contract = can.WorkStatus;
                if (can.Languages != null)
                {
                    foreach (Languages lang in can.Languages)
                    {
                        if (lang != null && lang.Name != null)
                        {
                            cd.Language?.Add(lang.Name);
                        }
                    }
                }

                if (can.MainSkills != null)
                {
                    foreach (MainSkills ms in can.MainSkills)
                    {
                        if (ms != null && ms.Name != null)
                        {
                            cd.Mainskills.Add(ms.Name);
                        }
                    }
                }



                cd.Gender = can.Gender;
                mycpd.Add(cd);
            }
            return mycpd.ToList();
        }
    }
}
