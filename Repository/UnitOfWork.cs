using HelperPlan.Models;
using HelperPlan.Repository.Implementations;
using HelperPlan.Repository.Interfaces;

namespace HelperPlan.Repository
{
    public class UnitOfWork : IUnitOFWork
    {
        public ICandidateRepository CandidateRepository { get; private set; }
        public IEmployeerRepository EmployeerRepository { get; private set; }
        public IJobRepository JobRepository { get; private set; }
        public IPlanRepository PlanRepository { get; private set; }
        public ISubscribtionRepository SubscribtionRepository { get; private set; }
        public IAspNetFaceBookUsersRepository AspNetFaceBookUsersRepository { get; set; }


        private Context Context;
        public UnitOfWork(Context context)
        {

            Context = context;
            CandidateRepository = new CandidateRepository(context);
            EmployeerRepository = new EmployerRepository(context);
            JobRepository = new JobRepository(context);
            PlanRepository = new PlanRepository(context);
            SubscribtionRepository = new SubscribtionRepository(context);
            AspNetFaceBookUsersRepository = new FaceBookUserRepository(context);
        }


    }
}
