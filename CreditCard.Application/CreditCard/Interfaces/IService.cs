using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Application.CreditCard.Interfaces
{
    public interface IService<Entity, EntityDto, UpdateEntityDto> where Entity : class
        where EntityDto : class
        where UpdateEntityDto : class
    {
        Task<Entity> Add(EntityDto entity);
        Task Update(Guid id, UpdateEntityDto entity);
        Task Delete(Guid id);
        Task<Entity> GetById(Guid id);
        Task<List<Entity>> GetAll();
    }
}
