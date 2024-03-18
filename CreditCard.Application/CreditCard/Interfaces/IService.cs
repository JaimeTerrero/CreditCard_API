using BankTech.CreditCard.Application.CreditCard.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Application.CreditCard.Interfaces
{
    public interface IService<Entity, EntityDto, UpdateEntityDto, ResponseDto> where Entity : class
        where EntityDto : class
        where UpdateEntityDto : class
        where ResponseDto : class
    {
        Task<ResponseDto> Add(EntityDto entity);
        Task Update(Guid id, UpdateEntityDto entity, CancellationToken cancellationToken);
        Task Delete(Guid id);
        Task<Entity> GetById(Guid id);
        Task<List<Entity>> GetAll();
        Task TransferCashAdvance(Guid id, CreditCardCashAdvanceDto entity);
    }
}
