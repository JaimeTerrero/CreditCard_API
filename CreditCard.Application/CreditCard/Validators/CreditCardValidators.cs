using CreditCard.Application.CreditCard.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTech.CreditCard.Application.CreditCard.Validators
{
    public class CreditCardValidators : AbstractValidator<CreditCardDto>
    {
        public CreditCardValidators()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty()
                .WithMessage("El id del cliente es obligatorio");

            RuleFor(x => x.AccountNumber)
                .NotEmpty()
                .WithMessage("El número de cuenta es obligatorio");

            RuleFor(x => x.OwnerName)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio");

            RuleFor(x => x.IssuerName)
                .NotEmpty()
                .WithMessage("El nombre del emisor es obligatorio");

            RuleFor(x => x.CreditLimit)
                .NotEmpty()
                .WithMessage("Se debe de indicar el límite de crédito");
        }
    }
}
