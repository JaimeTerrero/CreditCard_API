using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Domain
{
    public interface IRepositoryT<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity);
        Task DeleteAsync(Guid id);
        Task<Entity> GetByIdAsync(Guid id);
        Task<List<Entity>> GetAllAsync();
    }
}
