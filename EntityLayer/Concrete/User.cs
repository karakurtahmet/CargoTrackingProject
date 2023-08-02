using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string UserName { get; set; }

        [Required, StringLength(100)]
        public string Password { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public int FirmId { get; set; }
        public virtual Firm Firm { get; set; }

        public virtual ICollection<Demand> Demands { get; set; }


    }
}
