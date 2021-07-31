using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class ScheduledPayment : BaseModel
    {
        [Required]
        [MaxLength(66)]
        public string MemberPaymentId { get; set; }
        [Required]
        public decimal PaymentAmount { get; set; }
        [Required]
        public DateTimeOffset PaymentDueDate { get; set; }
        public DateTimeOffset? FulfiledDate { get; set; }
        public bool Fulfiled { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        [MaxLength(200)]
        public string Note { get; set; }

        public virtual MemberPayment MemberPayment { get; set; }
    }
}
