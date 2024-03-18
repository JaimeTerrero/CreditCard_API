using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTech.CreditCard.Application.CreditCard.DTOs
{
    public class CreditCardResponseDto
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        public long AccountNumber { get; set; }
        public string OwnerName { get; set; }
        public long CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CutoffDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public decimal AvailableWithOverdraft { get; set; }
        public int SecurityNumber { get; set; }
        public string IssuerName { get; set; }
        public long CreditLimit { get; set; }
        public decimal CashAdvance { get; set; }
        public bool Status { get; set; }
        public decimal BalanceToDate { get; set; }
        public long OriginalValue { get; set; }
        public decimal BalanceToCut { get; set; }
    }

}
