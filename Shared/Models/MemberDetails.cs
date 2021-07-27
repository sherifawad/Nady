using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Models
{
    public class MemberDetails : BaseModel
    {
        [Key, ForeignKey(nameof(Member))]
        public new string Id { get; set; }
        public string NickName { get; set; } = "Mister";
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public virtual Member Member { get; set; }
    }
}
