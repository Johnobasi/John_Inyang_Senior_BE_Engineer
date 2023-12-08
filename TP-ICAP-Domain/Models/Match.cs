using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_ICAP_Domain.Models
{
    public  class Match
    {
        public string OrderId { get; set; }
        public decimal Notional { get; set; }
        public int Volume { get; set; }
    }
}
