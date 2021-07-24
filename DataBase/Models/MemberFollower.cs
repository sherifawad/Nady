using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBase.Models
{
    public class MemberFollower : BaseModel
    {
        [Required]
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string RelationShip { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime RemovedDate { get; set; }
        public string Note { get; set; }
    }
}
