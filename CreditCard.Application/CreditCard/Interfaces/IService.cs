using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Application.CreditCard.Interfaces
{
    public interface IService<Entity, EntityDto> where Entity : class
        where EntityDto : class
    {
        Task<Entity> Add(EntityDto entity);
        Task Update(int id, EntityDto entity);
        Task Delete(int id);
        Task<Entity> GetById(int id);
        Task<List<Entity>> GetAll();
    }
}
