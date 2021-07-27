using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class MemberVisitor : BaseModel
    {
        [Required] 
        public string MemberId { get; set; }
        public VisitorType VisitorType { get; set; }
        public DateTime? AccessesDate { get; set; }
        public bool Used { get; set; }
        public Gate Gate { get; set; }
        public string Note { get; set; }
        public virtual Member Member { get; set; }
    }
}
