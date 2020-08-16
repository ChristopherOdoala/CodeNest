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
        public string Currency { get; set; }
        public float Percentage { get; set; }
    }

    public class ResultModel
    {
        public double newAmount { get; set; }
        public List<ValidationResult> errorList { get; set; }
    }
}

