using BussinesLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Concrete
{
    public class PackageManager : IPackageService
    {
        private readonly IPackageDal _packageDal;
        private readonly Context c = new Context();
        public PackageManager(IPackageDal packageDal)
        {
            _packageDal = packageDal;
        }

        public Package GetByID(int id)
        {
            return _packageDal.Get(x=>x.PackageId == id);
        }

        public List<Package> GetList()
        {
            return _packageDal.List();
        }

        public void PackageAdd(Package package)
        {
            _packageDal.Insert(package);
        }

        public void PackageDelete(Package package)
        {
            _packageDal.Delete(package);
        }

        public void PackageUpdate(int id, int statusId, int warehouseId, string StoreCode)
        {
            var package = _packageDal.Get(x => x.PackageId == id);
            package.PStatusID = statusId;
            package.WarehouseId = warehouseId;
            package.StoreCode = StoreCode; 
            _packageDal.Update(package);
        }

        public void PackageUpdate(Package package)
        {
            _packageDal.Update(package);
        }

        public int GetByPackageCountByStoreCode(string storeCode)
        {
            return c.Packages.Count(p=>p.StoreCode == storeCode);
        }
    }
}
