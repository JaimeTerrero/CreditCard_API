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
            RuleFor(x => x.OwnerName)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio");

            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .WithMessage("El número de la tarjeta es obligatorio");

            RuleFor(x => x.ExpirationDate)
                .NotEmpty()
                .WithMessage("La fecha de expiración es obligatoria");

            RuleFor(x => x.CutoffDate)
                .NotEmpty()
                .WithMessage("La fecha de corte es obligatoria");

            RuleFor(x => x.PaymentDueDate)
                .NotEmpty()
                .WithMessage("La fecha de vencimiento de pago es obligatoria");

            RuleFor(x => x.AvailableWithOverdraft)
                .NotEmpty()
                .WithMessage("El monto disponible con sobregiro es obligatorio");

            RuleFor(x => x.SecurityNumber)
                .NotEmpty()
                .WithMessage("El número de seguridad es obligatorio")
                .GreaterThan(0)
                .WithMessage("El número de seguridad debe de ser mayor que 0")
                .LessThan(5)
                .WithMessage("El número de seguridad debe de ser menor que 5");

            RuleFor(x => x.IssuerName)
                .NotEmpty()
                .WithMessage("El nombre del emisor es obligatorio");

            RuleFor(x => x.CreditLimit)
                .NotEmpty()
                .WithMessage("Se debe de indicar el límite de crédito");
        }
    }
}
