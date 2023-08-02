using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Abstract
{
    public interface IPackageTrackingService
    {
        List<PackageTracking> GetList();
        void PackageTrackingAdd(PackageTracking packageTracking);
        PackageTracking GetById(int  id);
        void PackageTrackingDelete(PackageTracking packageTracking);
    }
}
