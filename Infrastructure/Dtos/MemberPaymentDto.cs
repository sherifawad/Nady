using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Dtos
{
    public class MemberPaymentDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string MemberId { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsScheduled { get; set; }
        public int PaymentType { get; set; }
        public decimal PaymentAmount { get; set; }
        [Required]
        public decimal PaymentTotal { get; set; }
        public double TaxPercentage { get; set; }
        public double DiscountPercentage { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        public string Note { get; set; }
    }
}
