﻿using Shared.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Models
{
    public class MemberPayment : BaseModel
    {
        [Required]
        public string MemberId { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsScheduled { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal PaymentAmount { get; set; }
        [Required]
        public decimal PaymentTotal { get; set; }
        public double TaxPercentage { get; set; }
        public double DiscountPercentage { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public virtual IEnumerable<ScheduledPayment> ScheduledPayments { get; set; } = new List<ScheduledPayment>();

        public virtual Member Member { get; set; }
    }
}