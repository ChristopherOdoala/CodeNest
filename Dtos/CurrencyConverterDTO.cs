using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Dtos
{
    public class CurrencyConverterDTO
    {
        public Currencies FromCurrency { get; set; }
        public Currencies ToCurrency { get; set; }
        public double Amount { get; set; }
    }
}
