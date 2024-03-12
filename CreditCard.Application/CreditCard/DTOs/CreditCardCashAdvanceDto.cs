﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTech.CreditCard.Application.CreditCard.DTOs
{
    public class CreditCardCashAdvanceDto
    {
        public long AccountNumber { get; set; }
        public long CreditLimit { get; set; }
        public long CashAdvance { get; set; }
    }
}