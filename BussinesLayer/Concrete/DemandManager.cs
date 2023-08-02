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
    public class DemandManager : IDemandService
    {
        private readonly IDemandDal _demandDal;
        public DemandManager(IDemandDal demandDal) 
        {
            _demandDal = demandDal;
        }
        public void DemandAdd(Demand demand)
        {
            _demandDal.Insert(demand);
        }

        public void DemandDelete(Demand demand)
        {
            _demandDal.Delete(demand);
        }

        public void DemandUpdate(int id ,int statusId)
        {
            var demand = _demandDal.Get(x=>x.DemandId == id);
            demand.StatusId = statusId;
            _demandDal.Update(demand);
        }

        public void DemandUpdate(Demand demand)
        {
            _demandDal.Update(demand);
        }

        public Demand GetById(int id)
        {
            return _demandDal.Get(x=>x.DemandId == id);
        }

        public List<Demand> GetList()
        {
            return _demandDal.List();
        }
    }
}
