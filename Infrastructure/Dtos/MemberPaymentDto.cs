using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Dtos
{
    public class MemberPaymentDto
    {
        [MaxLength(66)]
        public string Id { get; set; }
        [Required]
        [MaxLength(66)]
        public string MemberId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int PaymentType { get; set; }
        public int? PaymentMethod { get; set; }
        [Required]
        public decimal PaymentTotal { get; set; }
        public double? TaxPercentage { get; set; }
        public double? DiscountPercentage { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
        [MaxLength(200)]
        public string Note { get; set; }
    }
}
