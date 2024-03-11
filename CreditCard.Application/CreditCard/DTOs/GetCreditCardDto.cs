using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTech.CreditCard.Application.CreditCard.DTOs
{
    public class GetCreditCardDto
    {
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public string OwnerName { get; set; }
        public long CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CutoffDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public long AvailableWithOverdraft { get; set; }
        public long SecurityNumber { get; set; }
        public string IssuerName { get; set; }
        public long CreditLimit { get; set; }
    }
}
