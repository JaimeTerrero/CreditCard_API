using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Application.CreditCard.DTOs
{
    public class CreditCardDto
    {
        public int ClientId { get; set; }
        public string IssuerName { get; set; }
        public long CreditLimit { get; set; }
    }
}
