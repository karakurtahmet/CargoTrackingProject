using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }
        [Required]
        public string Barcode { get; set; } = string.Empty;
        public bool is_active { get; set; } = true;
        public int PStatusID { get; set; }


        public int WarehouseId { get; set; }

        public string StoreCode { get; set; } = string.Empty;
        public int? DemandID { get; set; }
        //public virtual Store Store { get; set; }
        public virtual PackageStatus PackageStatus { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<PackageTracking> PackageTrackings { get; set; }



    }

}
