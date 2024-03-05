using CreditCard.Domain.Interfaces.Repositories.CreditCard;
using CreditCard.Infraestructure.Repositories.CreditCard;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Infraestructure.IoC
{
    public static class IoC
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<ICreditCardRepository, CreditCardRepository>();
        }
    }
}
