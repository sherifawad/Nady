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
        [MaxLength(66)]
        public string MemberId { get; set; }
        [Required]
        public VisitorType VisitorType { get; set; }
        [Required]
        public DateTimeOffset AddedDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? AccessesDate { get; set; }
        [Required]
        public VisitorStatus VisitorStatus { get; set; }
        public Gate? Gate { get; set; }
        [MaxLength(200)]
        public string Note { get; set; }
        public virtual Member Member { get; set; }
    }
}
