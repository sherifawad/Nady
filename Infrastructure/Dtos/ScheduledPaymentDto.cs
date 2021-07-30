using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Dtos
{
    public class ScheduledPaymentDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string MemberPaymentId { get; set; }
        [Required]
        public decimal PaymentAmount { get; set; }
        [Required]
        public DateTimeOffset PaymentDueDate { get; set; }
        public DateTimeOffset? FulfiledDate { get; set; }
        public bool Fulfiled { get; set; }
        public int PaymentMethod { get; set; }
        public string Note { get; set; }
    }
}
