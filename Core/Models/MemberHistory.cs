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
        public string MemberId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Detail { get; set; }

        public virtual Member Member { get; set; }
    }
}
