using AutoMapper;
using BankTech.CreditCard.Application.CreditCard.DTOs;
using BankTech.CreditCard.Domain.Entities;
using CreditCard.Application.CreditCard.DTOs;
using CreditCard.Application.CreditCard.Interfaces.CreditCard;
using CreditCard.Domain;
using CreditCard.Domain.Entities;
using CreditCard.Domain.Interfaces.Repositories.CreditCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Application.CreditCard.Services
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IMapper _mapper;

        public CreditCardService(ICreditCardRepository creditCardRepository, IMapper mapper)
        {
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
        }

        public async Task<CreditCardResponseDto> Add(CreditCardDto creditCardDto)
        {
            var creditCard = _mapper.Map<CreditCards>(creditCardDto);
            await _creditCardRepository.AddAsync(creditCard);
            var creditCardResponse = _mapper.Map<CreditCardResponseDto>(creditCard);
            return creditCardResponse;
        }

        public async Task Delete(Guid id)
        {
            await _creditCardRepository.DeleteAsync(id);
        }

        public async Task<List<CreditCards>> GetAll()
        {
            var creditCardList = await _creditCardRepository.GetAllAsync();

            return creditCardList;
        }

        public async Task<CreditCards> GetById(Guid id)
        {
            var creditCard = await _creditCardRepository.GetByIdAsync(id);

            if(creditCard == null)
            {
                throw new Exception("Tarjeta de crédito no encontrada");
            }

            CreditCards cr = new();
            cr.Id = creditCard.Id;
            cr.ClientId = creditCard.ClientId;
            cr.AccountNumber = creditCard.AccountNumber;
            cr.OwnerName = creditCard.OwnerName;
            cr.CardNumber = creditCard.CardNumber;
            cr.ExpirationDate = creditCard.ExpirationDate;
            cr.CutoffDate = creditCard.CutoffDate;
            cr.PaymentDueDate = creditCard.PaymentDueDate;
            cr.AvailableWithOverdraft = creditCard.AvailableWithOverdraft;
            cr.SecurityNumber = creditCard.SecurityNumber;
            cr.IssuerName = creditCard.IssuerName;
            cr.CreditLimit = creditCard.CreditLimit;
            cr.CashAdvance = creditCard.CashAdvance;
            cr.BalanceToDate = creditCard.BalanceToDate;
            cr.OriginalValue = creditCard.OriginalValue;
            cr.BalanceToCut = creditCard.BalanceToCut;

            return cr;
        }

        public async Task Update(Guid id, UpdateCreditCardDto creditCardDto, CancellationToken cancellationToken)
        {
            CreditCards creditCards = await _creditCardRepository.GetByIdAsync(id);

            _mapper.Map(creditCardDto, creditCards);

            await _creditCardRepository.UpdateAsync(creditCards, cancellationToken);
        }

        public async Task<Paginated<GetCreditCardDto>> GetPaginatedCreditCardsAsync(int page, int pageSize)
        {
            IQueryable<CreditCards> queryable = _creditCardRepository.GetAllCreditCardsQueryable();
            Paginated<CreditCards> paginatedResult = await _creditCardRepository.GetCreditCardsPaginatedAsync(queryable, page, pageSize);

            List<GetCreditCardDto> result = paginatedResult.Items != null
                ? paginatedResult.Items.Select(st => _mapper.Map<GetCreditCardDto>(st)).ToList() :
                [];

            return new Paginated<GetCreditCardDto>
            {
                Items = result,
                TotalItems = paginatedResult.TotalItems,
                PageSize = pageSize,
                CurrentPage = page
            };
        }

        public async Task TransferCashAdvance(Guid id, CreditCardCashAdvanceDto creditCardCashAdvanceDto)
        {
            var creditCard = await _creditCardRepository.GetByIdAsync(id);

            if (creditCard == null)
                throw new Exception("Tarjeta de crédito no encontrada");

            // Calcula el cargo adicional y lo convierte a long
            var additionalCharge = (long)(creditCardCashAdvanceDto.CashAdvance * 0.05m);

            // Verifica si el avance de efectivo más el cargo adicional es menor o igual al límite de crédito
            if (creditCardCashAdvanceDto.CashAdvance + additionalCharge > creditCard.CreditLimit)
                throw new Exception("El avance de efectivo excede el límite de crédito");

            // Actualiza el avance de efectivo, el límite de crédito y el cargo adicional
            creditCard.CashAdvance += creditCardCashAdvanceDto.CashAdvance + additionalCharge;
            creditCard.CreditLimit -= creditCardCashAdvanceDto.CashAdvance + additionalCharge;
            creditCard.AvailableWithOverdraft -= creditCardCashAdvanceDto.CashAdvance + additionalCharge;
            creditCard.BalanceToDate = creditCard.CashAdvance;

            await _creditCardRepository.TransferCashAdvance(creditCard);
        }


        public async Task<List<CreditCards>> GetCreditCardByClientId(int clientId)
        {
            var creditCards = await _creditCardRepository.GetCreditCardByClientId(clientId);

            if (!creditCards.Any())
                throw new Exception("No se pudo encontrar una tarjeta con este cliente");

            List<CreditCards> creditCardsList = new List<CreditCards>();

            foreach (var creditCard in creditCards)
            {
                CreditCards cr = new();
                cr.Id = creditCard.Id;
                cr.ClientId = creditCard.ClientId;
                cr.AccountNumber = creditCard.AccountNumber;
                cr.OwnerName = creditCard.OwnerName;
                cr.CardNumber = creditCard.CardNumber;
                cr.ExpirationDate = creditCard.ExpirationDate;
                cr.CutoffDate = creditCard.CutoffDate;
                cr.PaymentDueDate = creditCard.PaymentDueDate;
                cr.AvailableWithOverdraft = creditCard.AvailableWithOverdraft;
                cr.SecurityNumber = creditCard.SecurityNumber;
                cr.IssuerName = creditCard.IssuerName;
                cr.CreditLimit = creditCard.CreditLimit;
                cr.CashAdvance = creditCard.CashAdvance;
                cr.BalanceToDate = creditCard.BalanceToDate;
                cr.OriginalValue = creditCard.OriginalValue;
                cr.BalanceToCut = creditCard.BalanceToCut;

                creditCardsList.Add(cr);
            }

            return creditCardsList;
        }

    }
}
