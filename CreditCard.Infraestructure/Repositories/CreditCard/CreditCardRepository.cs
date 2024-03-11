using BankTech.CreditCard.Domain.Entities;
using CreditCard.Domain.Entities;
using CreditCard.Domain.Interfaces.Repositories.CreditCard;
using CreditCard.Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Infraestructure.Repositories.CreditCard
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly CreditCardDbContext _creditCardDbContext;

        public CreditCardRepository(CreditCardDbContext creditCardDbContext)
        {
            _creditCardDbContext = creditCardDbContext;
        }

        public async Task<CreditCards> AddAsync(CreditCards creditCards)
        {
            creditCards.AvailableWithOverdraft = CalculateAvailableWithOverdraft(creditCards.CreditLimit);
            await _creditCardDbContext.CreditCards.AddAsync(creditCards);
            await _creditCardDbContext.SaveChangesAsync();
            return creditCards;
        }

        public async Task DeleteAsync(Guid id)
        {
            var creditCards = await _creditCardDbContext.CreditCards.FindAsync(id);
            _creditCardDbContext.Set<CreditCards>().Remove(creditCards);
            await _creditCardDbContext.SaveChangesAsync();
        }

        public async Task<List<CreditCards>> GetAllAsync()
        {
            return await _creditCardDbContext.Set<CreditCards>().ToListAsync();
        }

        public async Task<CreditCards> GetByIdAsync(Guid id)
        {
            return await _creditCardDbContext.Set<CreditCards>().FindAsync(id);
        }

        public async Task UpdateAsync(CreditCards creditCards)
        {
            _creditCardDbContext.Entry(creditCards).State = EntityState.Modified;
            await _creditCardDbContext.SaveChangesAsync();
        }

        public long CalculateAvailableWithOverdraft(long availableWithOverdraft)
        {
            double overdraftPercentage = 0.10; // 10% expressed as a decimal
            long overdraftAmount = (long)(availableWithOverdraft * overdraftPercentage);
            long overdraftSum = overdraftAmount + availableWithOverdraft;
            return overdraftSum;
        }

        public async Task<Paginated<CreditCards>> GetCreditCardsPaginatedAsync(IQueryable<CreditCards> queryable, int page, int pageSize)
        {
            var totalItems = await queryable.CountAsync();

            var paginatedItems = await queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Paginated<CreditCards>
            {
                Items = paginatedItems,
                TotalItems = totalItems,
                PageSize = pageSize,
                CurrentPage = page
            };
        }

        public IQueryable<CreditCards> GetAllCreditCardsQueryable()
        {
            return _creditCardDbContext.Set<CreditCards>();
        }

        public async Task TransferCashAdvance(CreditCards creditCards)
        {
            _creditCardDbContext.Entry(creditCards).State = EntityState.Modified;
            await _creditCardDbContext.SaveChangesAsync();
        }
    }
}
