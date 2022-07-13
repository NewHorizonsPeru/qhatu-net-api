using Domain.MainModule.Entities;
using Domain.MainModule.IRepositories;
using Infrastructure.Data.Core.Context;
using Infrastructure.Data.Core.Repository;

namespace Infrastructure.Data.MainModule.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(QhatuContext context) : base(context)
        {
        }
    }
}
