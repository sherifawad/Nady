using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class BaseModel : IDatabaseItem<string>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(66)]
        public string Id { get; set; }
    }
}
