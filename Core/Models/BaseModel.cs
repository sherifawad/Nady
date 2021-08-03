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

        public string CreatedByUser { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string ModifiedByUser { get; set; }

        public DateTimeOffset ModifiedDate { get; set; }
    }
}
