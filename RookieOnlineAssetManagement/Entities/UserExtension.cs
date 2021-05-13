using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Entities
{
    public class UserExtension
    {
        [Key]
        public int ID { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        public short NumIncrease { get; set; }
    }
}
