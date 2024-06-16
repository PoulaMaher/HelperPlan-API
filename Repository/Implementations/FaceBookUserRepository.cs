using HelperPlan.Models;
using HelperPlan.Repository.Interfaces;

namespace HelperPlan.Repository.Implementations
{
    public class FaceBookUserRepository: Repository<AspNetFaceBookUsers>, IAspNetFaceBookUsersRepository
    {
        private readonly Context dbcontext;
        public FaceBookUserRepository(Context _dbcontext) : base(_dbcontext)
        {
           
            dbcontext = _dbcontext;
        }
    }
}
