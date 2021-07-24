using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.Models
{
    public class MemberPayment : BaseModel
    {
        [Required]
        public string MemberId { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Name { get; set; }
        public decimal PaymentAmount { get; set; }
        [Required]
        public decimal PaymentTotal { get; set; }
        public double TaxPercentage { get; set; }
        public double DiscountPercentage { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public ScheduledPayment? ScheduledPayment { get; set; }
    }
}
