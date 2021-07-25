using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.Models
{
    public class Member : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public MemberDetails MemberDetails { get; set; }
        public MemberHistory MemberHistory { get; set; }
        public bool IsBasic { get; set; }
        public string Code { get; set; }
        public string RelationShip { get; set; }
        public string Note { get; set; }
        public IEnumerable<MemberPayment> MemberPayments { get; set; }
    }
}
