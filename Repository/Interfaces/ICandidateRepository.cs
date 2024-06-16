using HelperPlan.DTO.dto;
using HelperPlan.Models;
using System.Linq.Expressions;

namespace HelperPlan.Repository.Interfaces
{
    public interface ICandidateRepository : IRepository<Candidate>
    {

        IEnumerable<Candidate> filteredcandidate(filtercandidateDTO fcd, string? includeprop = null);
        public IEnumerable<CandidatePageDTO> mapandget(IEnumerable<Candidate> mycan);
    }
}
