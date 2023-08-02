using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class DemandStatus
    {
        [Key]
        public int DemandStatusId { get; set; }
        [Required]
        public string DemandStatusName { get; set; }
    }
}
