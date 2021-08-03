using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataBase.Models
{
    public class Audit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string AuditType { get; set; }           /*Create, Update or Delete*/
        public string AuditUser { get; set; }           /*Log User*/
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}
