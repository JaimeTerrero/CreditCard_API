using CreditCard.Application.CreditCard.DTOs;
using CreditCard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Application.CreditCard.Interfaces.CreditCard
{
    public interface ICreditCardService : IService<CreditCards, CreditCardDto>
    {
    }
}
