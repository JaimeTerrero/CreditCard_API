using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Domain
{
    public interface IRepositoryT<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Entity entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Entity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Entity>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
