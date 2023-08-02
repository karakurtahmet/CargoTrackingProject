using BussinesLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Concrete
{
    public class DemandTrackingManager : IDemandTrackingService
    {
        IDemandTrackingDal _demandTrackingDal;
        public DemandTrackingManager(IDemandTrackingDal demandTrackingDal) 
        {
            _demandTrackingDal = demandTrackingDal;

        }
        public void DemandTrackingAdd(DemandTracking demandTracking)
        {
            _demandTrackingDal.Insert(demandTracking);
        }

        public void DemandTrackingDelete(DemandTracking demandTracking)
        {
            _demandTrackingDal.Delete(demandTracking);
        }

        public DemandTracking GetById(int id)
        {
            return _demandTrackingDal.Get(x=>x.DemandTrackingId == id);
        }

        public List<DemandTracking> GetList()
        {
           return _demandTrackingDal.List();
        }
    }
}
