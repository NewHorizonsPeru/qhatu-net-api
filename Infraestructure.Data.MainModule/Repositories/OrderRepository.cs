using Domain.MainModule.Entities;
using Domain.MainModule.IRepositories;
using Infrastructure.Data.Core.Context;
using Infrastructure.Data.Core.Repository;

namespace Infrastructure.Data.MainModule.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(QhatuContext context) : base(context)
        {

        }
    }
}