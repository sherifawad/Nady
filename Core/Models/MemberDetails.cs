using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class MemberDetails : BaseModel
    {
        [Key, ForeignKey(nameof(Member))]
        [MaxLength(66)]
        public new string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string NickName { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        [MaxLength(200)]
        public string Image { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        public virtual Member Member { get; set; }
    }
}
