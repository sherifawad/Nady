using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.Models
{
    public class MemberHistory : BaseModel
    {
        [Required]
        public string MemberId { get; set; }
        [Required]
        public DateTime Added { get; set; } = DateTime.Now;
        public DateTime? Ended { get; set; }
        public DateTime? Preserved { get; set; }
        public DateTime? Resumed { get; set; }
        public DateTime? Separated { get; set; }
    }
}
