using Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Dtos
{
    public class MemberVisitorDto
    {
        [MaxLength(66)]
        public string Id { get; set; }
        [Required]
        [MaxLength(66)]
        public string MemberId { get; set; }
        [Required]
        public int VisitorType { get; set; }
        public DateTimeOffset? AccessesDate { get; set; }
        [Required]
        public int VisitorStatus { get; set; }
        public int? Gate { get; set; }
        [MaxLength(200)]
        public string Note { get; set; }
    }
}
