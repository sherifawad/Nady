using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class MemberHistory : BaseModel
    {
        [Required]
        [MaxLength(66)]
        public string MemberId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;
        [MaxLength(200)]
        public string Detail { get; set; }

        public virtual Member Member { get; set; }
    }
}
