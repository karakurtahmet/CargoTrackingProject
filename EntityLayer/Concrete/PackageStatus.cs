using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class PackageStatus
    {
        [Key]
        public int PStatusId { get; set; }
        [Required]
        public string PStatusName { get; set; } = string.Empty;

        

    }
}
