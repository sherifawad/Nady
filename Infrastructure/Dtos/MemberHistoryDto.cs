using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Dtos
{
    public class MemberHistoryDto
    {
        public string Id { get; set; }
        [Required]
        public string MemberId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        public string Detail { get; set; }

    }
}
