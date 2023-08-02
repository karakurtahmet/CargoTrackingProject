using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        //max capacity
        public int Capacity { get; set; }
        //number of packages stored in warehouse
        public int CurrentOccupancy { get; set; }

        public virtual ICollection<Package> Packages { get; set; }

    }
}
