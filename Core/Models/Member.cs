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
        public string Name { get; set; }
        public MemberDetails MemberDetails { get; set; }
        public MemberStatus MemberStatus { get; set; }
        public virtual List<MemberHistory> MemberHistoriesList { get; set; }
        public bool IsOwner { get; set; }
        public string Code { get; set; }
        public string RelationShip { get; set; }
        public string Note { get; set; }
        public virtual List<MemberPayment> MemberPayments { get; set; } = new List<MemberPayment>();
    }
}
