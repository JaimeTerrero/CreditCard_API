using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTech.CreditCard.Application.CreditCard.DTOs
{
    public class CreditCardCashAdvanceDto
    {
        public long AccountNumber { get; set; } //Cuenta a la que se va a transferir
        public long CashAdvance { get; set; } //Monto que se va a transferir
    }
}
