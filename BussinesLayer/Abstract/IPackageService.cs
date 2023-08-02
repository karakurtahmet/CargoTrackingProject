using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Abstract
{
    public interface IPackageService
    {
        List<Package> GetList();
        Package GetByID(int id);
        void PackageAdd(Package package);
        void PackageDelete(Package package);
        void PackageUpdate(Package package);
    }
}
