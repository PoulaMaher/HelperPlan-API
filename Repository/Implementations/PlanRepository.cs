using HelperPlan.Models;
using HelperPlan.Repository.Interfaces;

namespace HelperPlan.Repository.Implementations
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        private readonly Context dbcontext;
        public PlanRepository(Context _dbcontext) : base(_dbcontext)
        {
            dbcontext = _dbcontext;
        }

    }
}
