 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Demand
    {
        [Key]
        public int DemandId { get; set; }
        public int Quantity { get; set; } = 0;
        public int UserId { get; set; }
        public DateTime DemandDate { get; set; }
        public string RejectionReason { get; set; } = string.Empty;
        
        public int StatusId { get; set; } = 0;
        public string StoreCode { get; set; } = string.Empty;

        

        public virtual User User { get; set; }



    }
}
