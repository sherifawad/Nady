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
        public string MemberPaymentId { get; set; }
        [Required]
        public decimal PaymentAmount { get; set; }
        [Required]
        public DateTime PaymentDueDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public string Note { get; set; }

        public virtual MemberPayment MemberPayment { get; set; }
    }
}
