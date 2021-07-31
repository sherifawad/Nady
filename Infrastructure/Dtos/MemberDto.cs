using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Dtos
{
    public class MemberDto
    {
        [MaxLength(66)]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string NickName { get; set; } = "Mr.";
        [MaxLength(50)]
        public string Phone { get; set; }
        [MaxLength(200)]
        public string Image { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        public MemberStatus MemberStatus { get; set; }
        [Required]
        public bool IsOwner { get; set; }
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public string RelationShip { get; set; }
        [MaxLength(200)]
        public string Note { get; set; }
    }
}
