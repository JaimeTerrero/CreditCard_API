using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTech.CreditCard.Application.CreditCard.DTOs
{
    public class UpdateCreditCardDto
    {
        public DateTime CutoffDate { get; set; }
        public long AvailableWithOverdraft { get; set; }
        public long CreditLimit { get; set; }
    }
}
