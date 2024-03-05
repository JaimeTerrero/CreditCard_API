using CreditCard.Application.CreditCard.Interfaces.CreditCard;
using CreditCard.Application.CreditCard.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Application.IoC
{
    public static class IoC
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            return services
                .AddScoped<ICreditCardService, CreditCardService>();
        }
    }
}
