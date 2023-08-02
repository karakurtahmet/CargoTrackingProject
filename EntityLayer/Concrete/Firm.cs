using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Firm
    {
        [Key] 
        public int FirmId { get; set; }
        [StringLength(50)] 
        public string FirmName { get; set; } = string.Empty;
        [StringLength(50)]
        public string FirmAddres { get; set; } = string.Empty;


        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<User> Users { get; set; }
        

    }
}
