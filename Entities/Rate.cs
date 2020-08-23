using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Rate
    {
        public int Id { get; set; }
        public Currencies CurrencyOne { get; set; }
        public float Percentage { get; set; }
        public Currencies CurrencyTwo { get; set; }
    }

    public class ResultModel
    {
        public double newAmount { get; set; }
        public List<ValidationResult> errorList { get; set; }
    }

    public enum Currencies
    {
        Naira = 1,
        Pounds,
        Euro,
        Rands,
        Yen
    }
}

