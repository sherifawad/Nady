using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Dtos
{
    public class MemberDto
    {

        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string NickName { get; set; } = "Mr.";
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public MemberStatus MemberStatus { get; set; }
        public bool IsOwner { get; set; }
        [Required]
        public string Code { get; set; }
        public string RelationShip { get; set; }
        public string Note { get; set; }
    }
}
