using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class DemandTracking
    {
        [Key]
        public int DemandTrackingId { get; set; }
        public int DemandId { get; set; }
        public int UserId { get; set; }
        public int DemandStatusId { get; set; }
        public DateTime IndexDate { get; set; } 
        public string ChangeDescription { get; set; }
        public virtual User User { get; set; }

    }
}
