using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoTracking.Models
{
    public class DemandViewModel
    {
        public Demand Demand { get; set; }
        public List<SelectListItem>StoreValues { get; set; }
        public int SelectedStoreId { get; set; } 
        public Dictionary<int,int> PackageCountByStore { get; set; }
    }
}