using AutoMapper;
using BankTech.CreditCard.Application.CreditCard.DTOs;
using CreditCard.Application.CreditCard.DTOs;
using CreditCard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Infraestructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreditCardDto, CreditCards>();

            CreateMap<UpdateCreditCardDto, CreditCards>();

            CreateMap<GetCreditCardDto, CreditCards>().ReverseMap();

            CreateMap<CreditCardCashAdvanceDto, CreditCards>().ReverseMap();
        }
    }
}
