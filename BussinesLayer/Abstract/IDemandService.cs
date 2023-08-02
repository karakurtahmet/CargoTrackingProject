using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Abstract
{
     public interface IDemandService
    {
        List<Demand> GetList();
        void DemandAdd(Demand demand);
        Demand GetById(int id);
        void DemandDelete(Demand demand);
        void DemandUpdate(Demand demand);
    }
}
