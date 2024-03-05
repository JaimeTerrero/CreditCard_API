using AutoMapper;
using CreditCard.Application.CreditCard.DTOs;
using CreditCard.Application.CreditCard.Interfaces.CreditCard;
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

        public async Task<CreditCards> Add(CreditCardDto creditCardDto)
        {
            var creditCard = _mapper.Map<CreditCards>(creditCardDto);
            await _creditCardRepository.AddAsync(creditCard);
            return creditCard;
        }

        public async Task Delete(int id)
        {
            await _creditCardRepository.DeleteAsync(id);
        }

        public async Task<List<CreditCards>> GetAll()
        {
            var creditCardList = await _creditCardRepository.GetAllAsync();

            return creditCardList;
        }

        public async Task<CreditCards> GetById(int id)
        {
            var creditCard = await _creditCardRepository.GetByIdAsync(id);

            CreditCards cr = new();
            cr.Id = creditCard.Id;
            cr.ClientId = creditCard.ClientId;
            cr.OwnerName = creditCard.OwnerName;
            cr.CardNumber = creditCard.CardNumber;
            cr.ExpirationDate = creditCard.ExpirationDate;
            cr.CutoffDate = creditCard.CutoffDate;
            cr.PaymentDueDate = creditCard.PaymentDueDate;
            cr.AvailableWithOverdraft = creditCard.AvailableWithOverdraft;
            cr.SecurityNumber = creditCard.SecurityNumber;
            cr.IssuerName = creditCard.IssuerName;
            cr.CreditLimit = creditCard.CreditLimit;

            return cr;
        }

        public async Task Update(int id, CreditCardDto creditCardDto)
        {
            CreditCards creditCards = await _creditCardRepository.GetByIdAsync(id);

            _mapper.Map(creditCardDto, creditCards);

            await _creditCardRepository.UpdateAsync(creditCards);
        }
    }
}
