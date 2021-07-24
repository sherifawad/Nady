using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Models
{
    public class Member : BaseModel
    {
        public MemberDetails MemberDetails { get; set; }
        public bool IsBasic { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PreserveDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<MemberPayment> MemberPayments { get; set; }
        public IEnumerable<MemberFollower> MemberFollower { get; set; }
    }
}
