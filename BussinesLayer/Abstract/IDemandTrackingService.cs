using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Abstract
{
    public interface IDemandTrackingService
    {
        List<DemandTracking> GetList();
        void DemandTrackingAdd(DemandTracking demandTracking);
        DemandTracking GetById(int id);
        void DemandTrackingDelete(DemandTracking demandTracking);
    }
}
