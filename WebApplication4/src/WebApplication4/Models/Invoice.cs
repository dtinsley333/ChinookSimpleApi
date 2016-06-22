using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceLine> InvoiceLine { get; set; }
    }
}
