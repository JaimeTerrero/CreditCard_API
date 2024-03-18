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
        private readonly Random _random;

        public CreditCardRepository(CreditCardDbContext creditCardDbContext)
        {
            _creditCardDbContext = creditCardDbContext;
            _random = new Random();
        }

        public async Task<CreditCards> AddAsync(CreditCards creditCards, CancellationToken cancellationToken)
        {
            creditCards.AccountNumber = GenerateBankAccountNumber();
            creditCards.OriginalValue = creditCards.CreditLimit;
            creditCards.CardNumber = await GenerateUniqueCreditCardNumber();
            creditCards.SecurityNumber = GenerateCVV();
            creditCards.CutoffDate = GenerateNextMonthCutoffDate();
            creditCards.PaymentDueDate = GeneratePaymentDueDate();
            creditCards.ExpirationDate = GenerateExpirationDate();
            creditCards.AvailableWithOverdraft = CalculateAvailableWithOverdraft(creditCards.CreditLimit);
            await _creditCardDbContext.CreditCards.AddAsync(creditCards, cancellationToken);
            await _creditCardDbContext.SaveChangesAsync(cancellationToken);
            return creditCards;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var creditCards = await _creditCardDbContext.CreditCards.FindAsync(id, cancellationToken);
            if (creditCards == null)
            {
                return;
            }

            _creditCardDbContext.Set<CreditCards>().Remove(creditCards);
            await _creditCardDbContext.SaveChangesAsync(cancellationToken);
        }


        public async Task<List<CreditCards>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _creditCardDbContext.Set<CreditCards>().ToListAsync(cancellationToken);
        }

        public async Task<CreditCards> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _creditCardDbContext.Set<CreditCards>().FindAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(CreditCards creditCards, CancellationToken cancellationToken)
        {
            _creditCardDbContext.Entry(creditCards).State = EntityState.Modified;
            await _creditCardDbContext.SaveChangesAsync(cancellationToken);
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

        public async Task TransferCashAdvance(CreditCards creditCards, CancellationToken cancellationToken)
        {
            _creditCardDbContext.Entry(creditCards).State = EntityState.Modified;
            await _creditCardDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<CreditCards>> GetCreditCardByClientId(int clientId)
        {
            return await _creditCardDbContext.Set<CreditCards>().Where(c => c.ClientId == clientId).ToListAsync();
        }

        public async Task ChangeCreditCardStatus(CreditCards creditCards, CancellationToken cancellationToken = default)
        {
            _creditCardDbContext.Entry(creditCards).State = EntityState.Modified;
            await _creditCardDbContext.SaveChangesAsync(cancellationToken);
        }

        #region generators

        public long CalculateAvailableWithOverdraft(long availableWithOverdraft)
        {
            double overdraftPercentage = 0.10;
            long overdraftAmount = (long)(availableWithOverdraft * overdraftPercentage);
            long overdraftSum = overdraftAmount + availableWithOverdraft;
            return overdraftSum;
        }

        private long GenerateBankAccountNumber()
        {
            long minAccountNumber = 100000000;
            long maxAccountNumber = 999999999;
            long accountNumber = (long)(_random.NextDouble() * (maxAccountNumber - minAccountNumber) + minAccountNumber);

            return accountNumber;
        }

        public async Task<long> GenerateUniqueCreditCardNumber()
        {
            long creditCardNumber = 0;

            do
            {
                for (int i = 1; i < 16; i++)
                {
                    int digit = _random.Next(1, 101);
                    creditCardNumber = creditCardNumber * 10 + digit;
                }
            } while (await _creditCardDbContext.CreditCards.AnyAsync(c => c.CardNumber == creditCardNumber));

            return creditCardNumber;
        }


        public long GenerateCVV()
        {
            int minCode = 100;
            int maxCode = 999;
            int secretCode = _random.Next(minCode, maxCode + 1);

            return secretCode;
        }

        public DateTime GenerateNextMonthCutoffDate()
        {
            DateTime currentDate = DateTime.Now;

            DateTime nextMonth = currentDate.AddMonths(1);

            DateTime statementCutoffDate = new DateTime(nextMonth.Year, nextMonth.Month, currentDate.Day);

            return statementCutoffDate;
        }

        public DateTime GeneratePaymentDueDate()
        {
            DateTime currentDate = DateTime.Now;

            DateTime nextMonth = currentDate.AddMonths(1);

            DateTime statementPaymentDueDate = new DateTime(nextMonth.Year, nextMonth.Month, currentDate.Day);

            statementPaymentDueDate = statementPaymentDueDate.AddDays(20);

            return statementPaymentDueDate;
        }

        public DateTime GenerateExpirationDate()
        {
            DateTime currentDate = DateTime.Now;

            DateTime nextYear = currentDate.AddYears(4);

            DateTime statementExpirationDate = new DateTime(nextYear.Year, nextYear.Month, currentDate.Day);

            return statementExpirationDate;
        }

        #endregion

    }
}
