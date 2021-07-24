using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.Models
{
    public class MemberDetails : BaseModel
    {
        [Required]
        public string MemberId { get; set; }
        [Required]
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
    }
}
