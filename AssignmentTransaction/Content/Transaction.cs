using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentTransaction.Content.Entity
{
    public class Transaction
    {
        public int Transaction_ID { get; set; }
        public string Transaction_Account { get; set; }
        public string Transaction_Description { get; set; }
        public string Transaction_CurrencyCode { get; set; }
        public double Transaction_Amount { get; set; }
    }
}