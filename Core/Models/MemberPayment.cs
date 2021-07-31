using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class MemberPayment : BaseModel
    {
        [Required]
        [MaxLength(66)]
        public string MemberId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        [Required]
        public decimal PaymentTotal { get; set; }
        public double? TaxPercentage { get; set; }
        public double? DiscountPercentage { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
        [MaxLength(200)]
        public string Note { get; set; }
        public virtual IEnumerable<ScheduledPayment> ScheduledPayments { get; set; } = new List<ScheduledPayment>();

        public virtual Member Member { get; set; }
    }
}
