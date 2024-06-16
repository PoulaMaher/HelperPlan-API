using HelperPlan.Repository.Interfaces;

namespace HelperPlan.Repository
{
    public interface IUnitOFWork
    {

        ICandidateRepository CandidateRepository { get; }
        IEmployeerRepository EmployeerRepository { get; }
        IJobRepository JobRepository { get; }
        IPlanRepository PlanRepository { get; }
        ISubscribtionRepository SubscribtionRepository { get; }
        IAspNetFaceBookUsersRepository AspNetFaceBookUsersRepository { get; set; }

    }
}
