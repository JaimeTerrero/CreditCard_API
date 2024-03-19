using BankTech.CreditCard.Application.CreditCard.DTOs;
using BankTech.CreditCard.Domain.Entities;
using CreditCard.Application.CreditCard.DTOs;
using CreditCard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Application.CreditCard.Interfaces.CreditCard
{
    public interface ICreditCardService : IService<CreditCards, CreditCardDto, UpdateCreditCardDto, CreditCardResponseDto>
    {
        Task<Paginated<GetCreditCardDto>> GetPaginatedCreditCardsAsync(int page, int pageSize);
        Task<List<CreditCardResponseDto>> GetCreditCardByClientId(int clientId);
    }
}
