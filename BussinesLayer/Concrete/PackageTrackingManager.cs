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
    public class PackageTrackingManager:IPackageTrackingService
    {
        IPackageTrackingDal _packageTrackingDal;
        public PackageTrackingManager(IPackageTrackingDal packageTrackingDal)
        {
            _packageTrackingDal = packageTrackingDal;
        }

        public PackageTracking GetById(int id)
        {
            return _packageTrackingDal.Get(x=>x.TrackingId == id);
        }

        public List<PackageTracking> GetList()
        {
            return _packageTrackingDal.List();
        }
        public List<PackageTracking> GetByBarcode(string barcode)
        {
            return _packageTrackingDal.List(x=>x.Barcode == barcode);
        }

        public void PackageTrackingAdd(PackageTracking packageTracking)
        {
            _packageTrackingDal.Insert(packageTracking);
        }

        public void PackageTrackingDelete(PackageTracking packageTracking)
        {
            _packageTrackingDal.Delete(packageTracking);
        }
    }
}
