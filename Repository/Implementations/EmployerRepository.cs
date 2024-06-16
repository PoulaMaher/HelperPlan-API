using HelperPlan.Models;
using HelperPlan.Repository.Interfaces;

namespace HelperPlan.Repository.Implementations
{
    public class EmployerRepository : Repository<Employer>, IEmployeerRepository
    {
        private readonly Context dbcontext;
        public EmployerRepository(Context _dbcontext) : base(_dbcontext)
        {
            dbcontext = _dbcontext;
        }
    }
}
