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
    public class DemandStatusManager : IDemandStatusService
    {
        IDemandStatusDal _demandStatusDal;
        public DemandStatusManager(IDemandStatusDal demandStatusDal)
        {
            _demandStatusDal = demandStatusDal;
        }
        public DemandStatus GetById(int id)
        {
            return _demandStatusDal.Get(x=>x.DemandStatusId == id);
        }

        public List<DemandStatus> GetList()
        {
            return _demandStatusDal.List();
        }
    }
}
