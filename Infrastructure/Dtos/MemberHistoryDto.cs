﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Dtos
{
    public class MemberHistoryDto
    {

        [Required]
        [MaxLength(66)]
        public string Id { get; set; }
        [Required]
        [MaxLength(66)]
        public string MemberId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        [MaxLength(200)]
        public string Detail { get; set; }

    }
}
