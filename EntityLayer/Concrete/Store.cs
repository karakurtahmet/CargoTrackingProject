using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string StoreCode { get; set; } = string.Empty;

        public int FirmId { get; set; }
        public virtual Firm Firm { get; set; }

        // Navigation properties for relationships
        //public virtual ICollection<Package> Packages { get; set; }
    }

}
