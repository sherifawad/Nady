using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models
{
    public class Member : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public MemberDetails MemberDetails { get; set; }
        public MemberStatus MemberStatus { get; set; }
        public virtual List<MemberHistory> MemberHistoriesList { get; set; } = new List<MemberHistory>();
        [Required]
        public bool IsOwner { get; set; }
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public string RelationShip { get; set; }
        [MaxLength(200)]
        public string Note { get; set; }
        public virtual List<MemberPayment> MemberPayments { get; set; } = new List<MemberPayment>();
    }
}
