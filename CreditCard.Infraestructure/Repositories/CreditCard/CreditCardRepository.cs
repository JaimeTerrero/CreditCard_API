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
            await _creditCardDbContext.CreditCards.AddAsync(creditCards);
            await _creditCardDbContext.SaveChangesAsync();
            return creditCards;
        }

        public async Task DeleteAsync(int id)
        {
            var creditCards = await _creditCardDbContext.CreditCards.FindAsync(id);
            _creditCardDbContext.Set<CreditCards>().Remove(creditCards);
            await _creditCardDbContext.SaveChangesAsync();
        }

        public async Task<List<CreditCards>> GetAllAsync()
        {
            return await _creditCardDbContext.Set<CreditCards>().ToListAsync();
        }

        public async Task<CreditCards> GetByIdAsync(int id)
        {
            return await _creditCardDbContext.Set<CreditCards>().FindAsync(id);
        }

        public async Task UpdateAsync(CreditCards creditCards)
        {
            _creditCardDbContext.Entry(creditCards).State = EntityState.Modified;
            await _creditCardDbContext.SaveChangesAsync();
        }
    }
}
