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
    public class PackageStatusManager : IPsService
    {
        IPsDal _psDal;
        public PackageStatusManager(IPsDal psdal)
        {
            _psDal = psdal;
        }
        public PackageStatus GetById(int id)
        {
            return _psDal.Get(x=>x.PStatusId == id);
        }

        public List<PackageStatus> GetList()
        {
            return _psDal.List();
        }
    }
}
