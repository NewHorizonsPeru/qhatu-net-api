using Domain.Core.IRepository;
using Domain.MainModule.Entities;

namespace Domain.MainModule.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        
    }
}