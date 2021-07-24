using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.Models
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
    }
}
