using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Domain.Entities
{
    public class CreditCards : BaseEntity
    {
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public long AccountNumber { get; set; } // Each creditcard will be associated to an account
        public string OwnerName { get; set; }
        public long CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CutoffDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public long AvailableWithOverdraft { get; set; }
        public long SecurityNumber { get; set; }
        public string IssuerName { get; set; }
        public long CreditLimit { get; set; }
        public long CashAdvance { get; set; }
        public bool Status { get; set; } = true;
        public long BalanceToDate { get; set; }
        public long OriginalValue { get; set; } // El valor original de la tarjeta de crédito
        public long BalanceToCut { get; set; }
    }
}
