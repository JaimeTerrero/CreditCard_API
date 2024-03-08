using BankTech.CreditCard.Application.CreditCard.Validators;
using CreditCard.Application.CreditCard.DTOs;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankTech.CreditCard.Application.Config
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<CreditCardDto>, CreditCardValidators>();
        }
    }
}
