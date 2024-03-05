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
        Task DeleteAsync(int id);
        Task<Entity> GetByIdAsync(int id);
        Task<List<Entity>> GetAllAsync();
    }
}
