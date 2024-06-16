using HelperPlan.Models;
using HelperPlan.Repository.Interfaces;

namespace HelperPlan.Repository.Implementations
{
    public class SubscribtionRepository : Repository<Subscribtion>, ISubscribtionRepository
    {
        public SubscribtionRepository(Context _context) : base(_context)
        {
            
        }

    }
}
