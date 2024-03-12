using BankTech.CreditCard.Domain.Entities;
using CreditCard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CreditCard.Domain.Interfaces.Repositories.CreditCard
{
    public interface ICreditCardRepository : IRepositoryT<CreditCards>
    {
        Task<Paginated<CreditCards>> GetCreditCardsPaginatedAsync(IQueryable<CreditCards> queryable, int page, int pageSize);
        IQueryable<CreditCards> GetAllCreditCardsQueryable();
        Task TransferCashAdvance(CreditCards creditCards);
        Task<List<CreditCards>> GetCreditCardByClientId(int clientId);
    }
}
