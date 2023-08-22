using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSIS.ViewModels
{
    public class TransactionVM
    {
        public TransactionVM() { }
        public string TransactionID { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Status { get; set; }
    }
}